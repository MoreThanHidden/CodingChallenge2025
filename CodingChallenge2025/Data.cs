using CsvHelper.Configuration.Attributes;

namespace CodingChallenge2025;

///<summary>
/// Data Definition Class
/// </summary>
public class Data
{
    [Name("Device ID")]
    public required int DeviceId { get; set; }
    [Name("Rainfall")]
    public required double DataValue { get; set; }
    [Name("Time")]
    public required DateTime Timestamp { get; set; }
    
}