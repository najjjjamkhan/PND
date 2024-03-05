using System.ComponentModel.DataAnnotations;

namespace PND.Models
{
    public class Appointment
    {

        [Required]
        [Key]
        public int ApID { get; set; }
        [Required]
        public int PatientID { get; set; }
        public string? PatientName { get; set; }
        [Required]
        public DateTime? ApDate { get; set; }

    }
}
