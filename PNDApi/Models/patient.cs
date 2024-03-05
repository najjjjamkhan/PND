using System.ComponentModel.DataAnnotations;

namespace PNDApi.Models
{
    public class patient
    {
        [Key]
        public int PatientID { get; set; }

        public string? PatientName { get; set; } = null!;

        public string Disease { get; set; } = null!;

        public string Address { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;

    }
}
