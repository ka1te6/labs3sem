using System.ComponentModel.DataAnnotations.Schema;

public class Price
{
    public int Id { get; set; }

    [ForeignKey("Stock")]
    public int TickerId { get; set; }

    public double PriceValue { get; set; }
    public DateTime Date { get; set; }

    public virtual Stock Stock { get; set; }
}