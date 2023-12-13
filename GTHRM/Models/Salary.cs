using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTHRM.Models
{
    public class Salary
    {
        [Required]
        public Guid ComId { get; set; }
        [Required]
        public Guid EmpId { get; set; }

        [Required]
        [DisplayName("Year")]
        public int year { get; set; }
        [Required]
        [DisplayName("Month")]
        public int month { get; set; }

        [DisplayName("Salary")]
        public double gross { get; set; }
        public double basic { get; set; }
        public double hrent { get; set; }
        public double medical { get; set; }
        [DisplayName("Absent Amount")]
        public double absentAmount { get; set; }
        [DisplayName("Payable Amount")]
        public double payableAmount { get; set; }
        public Boolean isPaid { get; set; }
        [DisplayName("Paid Amount")]
        public double paidAmount { get; set; }

        [ValidateNever,ForeignKey("ComId")]
        public Company company { get; set; }

        [ValidateNever, ForeignKey("EmpId")]
        public Employee Employee { get; set; }

    }
}
