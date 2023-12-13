using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTHRM.Models
{
    public class Employee
    {
        [Key]
        [DisplayName("Employee Id")]
        public Guid EmpId { get; set; }= Guid.NewGuid();

        [Required]
        public Guid ComId { get; set; }
        [Required]
        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }
        [Required]
        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [Required]
        public Guid DeptId { get; set; }
        [Required]
        public Guid DesigId { get; set; }
        [Required]
        public Guid ShiftId { get; set; }

        [Required]
        public Gender gender { get; set; }
        [Required]
        [Range(9999,Double.MaxValue,ErrorMessage = "The Gross has to be greater then 9999.")]
        [DisplayName("Salary")]
        public decimal Gross { get; set; }
        public decimal Basic { get; set; }
        [DisplayName("House Rent")]
        public decimal Hrent { get; set; }
        public decimal Medical { get; set; }
        public decimal Others { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{DD:MM:YYYY}")]
        [DisplayName("Join Date")]
        public DateTime dtJoin { get; set; }



        [ValidateNever]
        [ForeignKey("ComId")]
        public Company Company { get; set; }

        [ValidateNever]
        [ForeignKey("DeptId")]
        public  Department Department { get; set; }

        [ValidateNever]
        [ForeignKey("DesigId")]
        public  Designation Designation { get; set; }

        [ValidateNever]
        [ForeignKey("ShiftId")]
        public  Shift Shift { get; set; }


    }
}
public enum Gender
{
    Male,
    Female,
    Other
}
