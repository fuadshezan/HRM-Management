using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GTHRM.Models
{
    public class Designation
    {
        [Key]
        public Guid DesigId { get; set; }= Guid.NewGuid();

        [Required]
        public Guid ComId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Designation Name")]
        public string DesigName { get; set; }

        [ValidateNever]
        [ForeignKey("ComId")]
        public Company Company { get; set; }

        
    }
}
