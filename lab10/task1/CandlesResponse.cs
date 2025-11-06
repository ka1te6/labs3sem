using System.Collections.Generic;
using System.Text.Json.Serialization;

public class CandlesResponse
{
    [JsonPropertyName("s")]
    public string? Status { get; set; }

    [JsonPropertyName("c")]
    public List<double>? Close { get; set; }

    [JsonPropertyName("h")]
    public List<double>? High { get; set; }

    [JsonPropertyName("l")]
    public List<double>? Low { get; set; }

    [JsonPropertyName("o")]
    public List<double>? Open { get; set; }

    [JsonPropertyName("t")]
    public List<long>? Timestamp { get; set; }

    [JsonPropertyName("v")]
    public List<double>? Volume { get; set; } 
}

public class StockData
{
    public string? Ticker { get; set; }
    public double AveragePrice { get; set; }
}