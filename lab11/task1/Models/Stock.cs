using System;

public class Stock
{
    public int Id { get; set; }         
    public string Ticker { get; set; } = "";
    public DateTime Date { get; set; }
    public double ClosePrice { get; set; }
}