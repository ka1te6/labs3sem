using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab13App.Models;

public class Researcher
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Position { get; set; }

    [EmailAddress, StringLength(100)]
    public string? Email { get; set; }

    public ICollection<Experiment>? Experiments { get; set; }
}

