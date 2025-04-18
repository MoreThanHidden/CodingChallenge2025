namespace CodingChallenge2025;

/// <summary>
///  Interface for output handling.
/// </summary>
public interface IOutputHandler
{
    void DisplayResult(Device device, RainfallResult result);
}

/// <summary>
///  Console output handler implementation.
/// </summary>
public class ConsoleOutputHandler : IOutputHandler
{
    /// <summary>
    /// Displays the result of rainfall data processing for a given device.
    /// </summary>
    /// <param name="device">The device to be output.</param>
    /// <param name="result">The result data processing.</param>
    public void DisplayResult(Device device, RainfallResult result)
    {
        // Write device information to the console
        Console.WriteLine($"DeviceId: {device.DeviceId}; DeviceName: {device.DeviceName}; Location: {device.Location};");

        // Set console color based on the status of the result
        Console.ForegroundColor = result.Status switch
        {
            "Green" => ConsoleColor.Green,
            "Amber" => ConsoleColor.Yellow,
            _ => ConsoleColor.Red
        };

        // Write the result information to the console and reset the color
        Console.WriteLine($"Average rainfall {result.AverageRainfall:F2}mm; Status: {result.Status}; Trend: {result.Trend};");
        Console.ResetColor();
    }
}