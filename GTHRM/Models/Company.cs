using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace GTHRM.Models
{
    [Index(nameof(CompanyName), IsUnique = true)]
    public class Company
    {
        [Key]
        public Guid ComId { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [DisplayName("Basic(%)")]
        [Range(0,100)]
        public decimal Basic { get; set; }
        [DisplayName("House Rent(%)")]
        [Range(0,100)]
        public decimal Hrent { get; set; }
        [DisplayName("Medical(%)")]
        [Range(0,100)]
        public decimal Medical { get; set; }

        [DisplayName("Others(%)")]
        [Range(0,100)]
        public decimal Others { get; set; }

        [DisplayName("Isactive?")]
        public Boolean IsActive { get; set; }
        
    }
}
