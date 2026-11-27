using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Models;

public class DepartmentDetail
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string DepartmentName { get; set; }
}
