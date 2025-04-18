using CsvHelper.Configuration.Attributes;

namespace CodingChallenge2025;

///<summary>
/// Device Definition Class
/// </summary>
public class Device
{
    [Name("Device ID")]
    public required int DeviceId { get; set; }
    [Name("Device Name")]
    public required string DeviceName { get; set; }
    public required string Location { get; set; }
}