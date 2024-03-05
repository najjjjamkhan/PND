using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PND.Models
{
    public class Doctor
    {
        
        [Key]
        public int Dr_ID { get; set; }
        [Required]
        [DisplayName("Doctor Name")]
        public string DrName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Specification { get; set; } 

    }
}
