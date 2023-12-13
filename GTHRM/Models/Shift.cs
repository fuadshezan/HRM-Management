using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GTHRM.Models
{
    public class Shift
    {

        [Key]
        public Guid ShiftId { get; set; }

        [Required]
        public Guid ComId { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Shift Name")]
        public string ShiftName { get; set; }

        [Required]
        [DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        [DisplayName("Shift Start Time")]
        public DateTime ShiftIn { get; set; }

        [Required]
        [DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        [DisplayName("Shift End Time")]
        public DateTime ShiftOut { get; set; }

        [Required]
        [DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:H:mm}")]
        [DisplayName("Shift Late Time")]
        public DateTime ShiftLate { get; set; }


        [ValidateNever]
        [ForeignKey("ComId")]
        public virtual Company Company { get; set; }
    }
}
