using System;

public class TodaysCondition
{
    public int Id { get; set; }
    public string Ticker { get; set; } = "";
    public string ChangeStatus { get; set; } = ""; 
    public DateTime DateChecked { get; set; }
}