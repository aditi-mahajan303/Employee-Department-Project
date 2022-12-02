using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpDeptProject.Models.Models
{
    public class Employee
    {
        [Key]
        [DisplayName("Employee Id")]
        public int EmpId { get; set; }
        
        
        [Required(ErrorMessage = "Employee Name is required.")]
        [DisplayName("Employee Name")]
        [RegularExpression("^((?!City)[a-zA-Z '])+$", ErrorMessage = "Employee Name can only contain characters.")]
        [StringLength(20)]
        public string? EmpName { get; set; }

        
        [Required(ErrorMessage = "Employee Salary is required.")]
        [DisplayName("Employee Salary")]
        public int EmpSalary { get; set; }

        
        [DisplayName("Department Name")] 
        public int DeptId { get; set; }

        
        [ForeignKey("DeptId")]
        public Department? Departments { get; set; }
    }
}
