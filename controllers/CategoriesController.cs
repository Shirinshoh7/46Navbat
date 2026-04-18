using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _46Navbat.Data;
using _46Navbat.Models;

namespace _46Navbat.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;
    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return Ok(category);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var list = await _context.Categories.ToListAsync();
        return Ok(list);
    }


}