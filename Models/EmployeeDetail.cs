using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Models;

public class EmployeeDetail
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public int DepartmentId { get; set; }
}
