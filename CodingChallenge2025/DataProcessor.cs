namespace CodingChallenge2025;

/// <summary>
///  Interface for processing rainfall data.
/// </summary>
public interface IDataProcessor
{
    RainfallResult ProcessData(Device device, List<Data> data);
}

/// <summary>
///  Rainfall data processor implementation.
/// </summary>
public class RainfallDataProcessor : IDataProcessor
{
    /// <summary>
    ///  Processes rainfall data for a given device.
    /// </summary>
    /// <param name="device">Device to be processed</param>
    /// <param name="data">Data to be processed</param>
    /// <returns>Rainfall Results</returns>
    public RainfallResult ProcessData(Device device, List<Data> data)
    {
        // Filter data for the specific device
        var deviceData = data.Where(x => x.DeviceId == device.DeviceId).ToList();
        
        // Return an empty result if no data is found
        if (!deviceData.Any()) return new RainfallResult(0, "N/A", "Green");

        // Get the last timestamp and filter data for the last 4 hours
        DateTime lastTimeStamp = deviceData.Max(x => x.Timestamp);
        var last4Hours = deviceData.Where(x => x.Timestamp >= lastTimeStamp.AddHours(-4)).ToList();

        // If no data is found in the last 4 hours, return a default result
        double avgRainfall = last4Hours.Average(x => x.DataValue);
        
        //Trend Increasing or Decreasing based on average of first 2 values vs last 2 values
        string trend = last4Hours.Count > 3
            ? (last4Hours.Take(2).Average(x => x.DataValue) < last4Hours.Skip(2).Average(x => x.DataValue) ? "Increasing" : "Decreasing")
            : "N/A";

        // Determine the rainfall status based on average rainfall
        string rainfallStatus = avgRainfall switch
        {
            < 10 => "Green",
            < 15 => "Amber",
            _ => "Red"
        };

        // Check if any data points in the last 4 hours exceed 30mm and set status to Red
        if (last4Hours.Any(x => x.DataValue > 30))
        {
            rainfallStatus = "Red";
        }

        // Return the rainfall result with average rainfall, trend, and status
        return new RainfallResult(avgRainfall, trend, rainfallStatus);
    }
}

/// <summary>
///  Rainfall result data structure.
/// </summary>
public record RainfallResult(double AverageRainfall, string Trend, string Status);