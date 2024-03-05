using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PNDApi.Models;

public partial class Doctor
{
    [Key]
    public int Dr_ID { get; set; }

    public string DrName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Specification { get; set; } = null!;
}
