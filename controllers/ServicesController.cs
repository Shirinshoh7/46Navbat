namespace _46Navbat.Controllers;

using Microsoft.EntityFrameworkCore;
using _46Navbat.Data;
using _46Navbat.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class ServicesController : ControllerBase
{
    private readonly AppDbContext _context;
    public ServicesController(AppDbContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<IActionResult> CreateService(Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        return Ok(service);
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        var services = await _context.Services.ToListAsync();
        return Ok(services);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id)

    {
        var service = await _context.Services.FindAsync(id);
        if (service == null) return NotFound();

        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
        return Ok();
    }

    
}