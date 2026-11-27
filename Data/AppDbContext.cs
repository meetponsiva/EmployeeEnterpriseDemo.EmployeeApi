using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<EmployeeDetail> EmployeeDetails => Set<EmployeeDetail>();

    public DbSet<DepartmentDetail> DepartmentDetails => Set<DepartmentDetail>();
}
