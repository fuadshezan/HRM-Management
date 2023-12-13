using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GTHRM.Models
{
    public class Department
    {
        [Key]
        public Guid DeptId { get; set; }

        [Required]
        public Guid ComId { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }

        [ValidateNever]
        [ForeignKey("ComId")]
        public virtual Company Company { get; set; }
    }
}
