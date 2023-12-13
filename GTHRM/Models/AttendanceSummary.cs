using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTHRM.Models
{
    public class AttendanceSummary
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

        [DisplayName("Present")]
        public int present { get; set; }
        [DisplayName("Late")]
        public int late { get; set; }
        [DisplayName("Absent")]
        public int absent { get; set; }

        [ValidateNever]
        [ForeignKey("ComId")]
        public Company Company { get; set; }
        [ValidateNever]
        [ForeignKey("EmpId")]
        public Employee Employee { get; set; }
    }
}
