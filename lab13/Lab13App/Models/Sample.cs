using System;
using System.ComponentModel.DataAnnotations;

namespace Lab13App.Models;

public class Sample
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string SampleCode { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Type { get; set; }

    [DataType(DataType.Date)]
    public DateTime CollectedDate { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    public int ExperimentId { get; set; }
    public Experiment? Experiment { get; set; }
}

