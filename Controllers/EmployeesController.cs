using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(AppDbContext db, IConfiguration config) : ControllerBase
{
    // SELECT (GET /api/employees)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        //var employees = await db.EmployeeDetails.OrderByDescending(e => e.Id).ToListAsync();
        //var employees = await db.EmployeeDetails.FromSqlRaw("EXEC [dbo].[sp_GetAllEmployees]").OrderBy(e => e.Id).AsNoTracking().ToListAsync();
        var employees = await db.EmployeeDetails.FromSqlInterpolated($"EXEC [dbo].[sp_GetAllEmployees]").AsNoTracking().ToListAsync();

        return Ok(new
        {
            Environment = config["ENVIRONMENT_NAME"] ?? "LOCAL",
            Employees = employees
        });
    }

    // CREATE (POST /api/employees)
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeDetail employee)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        db.EmployeeDetails.Add(employee);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = employee.Id }, employee);
    }
}
