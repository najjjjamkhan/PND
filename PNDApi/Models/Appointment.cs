using System.ComponentModel.DataAnnotations;

namespace PNDApi.Models
{
    public class Appointment
    {
        [Required]
        [Key]
        public int ApID { get; set; }
        [Required]
        public int PatientID { get; set; }
        ////[Required]
        public string PatientName { get; set; }
        [Required]
        public DateTime? ApDate { get; set; }
    }
}
