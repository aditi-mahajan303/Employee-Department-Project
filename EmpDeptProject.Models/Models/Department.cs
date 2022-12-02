using System.ComponentModel.DataAnnotations;

namespace EmpDeptProject.Models.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        [StringLength(20)]
        [Required(ErrorMessage = "Department Name is required.")]
        public string? DeptName { get; set; }

        
    }
}
