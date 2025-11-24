using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab13App.Models;

public class Experiment
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? EndDate { get; set; }

    [Display(Name = "Principal Investigator")]
    public int PrincipalInvestigatorId { get; set; }
    public Researcher? PrincipalInvestigator { get; set; }

    public ICollection<Sample>? Samples { get; set; }
}

