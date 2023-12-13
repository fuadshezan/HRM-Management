using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTHRM.Models
{
    public class Attendance
    {
        [Required]
        public Guid ComId { get; set; }
        
        [Required]
    
        public Guid EmpId { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime dtDate { get; set; }

        [DisplayName("Attendance Status")]
        public string? appStatus { get; set; }
        
        [Required]
        [DataType(DataType.Time)]
        [DisplayName("Entry Time")]
        public TimeSpan inTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayName("Exit Time")]
        public TimeSpan outTime { get; set; }

        [ValidateNever, ForeignKey("ComId")]
        public Company Company { get; set; }

        [ValidateNever,ForeignKey("EmpId")]
        public Employee Employee { get; set; }

    }
}
