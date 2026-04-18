using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _46Navbat.Data;
using _46Navbat.Models;

namespace _46Navbat.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TicketsController : ControllerBase
{
    private readonly AppDbContext _context;
    public TicketsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket(int serviceId)
    {
        var service = await _context.Services.FindAsync(serviceId);
        if (service == null)
        {
            return NotFound("Услуга не найдена");
        }
        var count = await _context.Tickets.CountAsync(t => t.ServiceId == serviceId);
        string fullNumber = $"{service.Prefix}-{count + 1}";

        var ticket = new Ticket
        {
            FullNumber = fullNumber,
            ServiceId = serviceId,
            Status = "waiting",
            CreatedAT = DateTime.UtcNow,
            PhoneNumber = "000"
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
        return Ok(ticket);
        
    }
    [HttpGet]
    public async Task<IActionResult> GetTickets()
    {
        var tickets = await _context.Tickets
        .Include(t => t.Service)
        .ToListAsync();

        return Ok(tickets);
    }

    [HttpPut("next/{serviceId}")]
    public async Task<IActionResult> CallNext(int serviceId)
    {
        var ticket = await _context.Tickets.Where(t => t.ServiceId == serviceId && t.Status == "waiting")
        .OrderBy(t => t.CreatedAT)
        .FirstOrDefaultAsync();

        if (ticket != null)
        {
            ticket.Status = "calling";
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }
        return NotFound("В очереди никого нет");

    }

    [HttpPut("complete/{serviceId}")]
    public async Task<IActionResult> CompleteTicket(int serviceId)
    {
        var ticket = await _context.Tickets.Where(t => t.ServiceId == serviceId && t.Status == "calling")
        .OrderBy(t => t.CreatedAT)
        .FirstOrDefaultAsync();
        if (ticket != null)
        {
            ticket.Status = "completed";
            await _context.SaveChangesAsync();
            return Ok(ticket);
        }
        return NotFound("Нет активных вызовов для этой услуги");
    }

}