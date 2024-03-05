using System.ComponentModel.DataAnnotations;

namespace PND.Models
{
    public class Patient
    {
        [Required]
        [Key]
        public int PatientID { get; set; }
        [Required]
        public string PatientName { get; set; } = null!;
        [Required]
        public string Disease { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
    }
}
