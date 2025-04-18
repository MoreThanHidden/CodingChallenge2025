using CsvHelper.Configuration.Attributes;

namespace CodingChallenge2025;

using CsvHelper;
using System.Globalization;

internal static class Program
{
    ///<summary>
    /// Coding Challenge EntryPoint 
    /// Directory Selection by command line arguments or current directory is used.
    /// CSVs "Data*.csv","Devices.csv" in the directory.
    ///</summary>
    ///<param name="args">Command line input (Directory Path for CSV Files)</param>
    private static void Main(string[] args)
    {

        //declare input variable
        string dirPath = ".";

        // Check for args (Challenge Selection)
        if (args.Length > 0)
        {
            // If arguments are provided, use first as directory input
            dirPath = args[0];
        }
        
        Console.ForegroundColor = ConsoleColor.Green;
        // Inform the user of the directory being used
        Console.WriteLine($"Reading Data from: {dirPath}\\");
        Console.ResetColor();
        
        // Read the CSV files into lists of records
        List<Device> devices = ReadDeviceCsv($"{dirPath}\\Devices.csv");
        List<Data> data = ReadDataCsv($"{dirPath}\\Data1.csv");
        data.AddRange(ReadDataCsv($"{dirPath}\\Data2.csv"));
        
        // Foreach device, find the corresponding data records and print average rainfall for past 4 hours and whether it is increasing or decreasing
        foreach (var device in devices)
        {
            // Print device information
            Console.WriteLine($"DeviceId: {device.DeviceId}; DeviceName: {device.DeviceName}; Location: {device.Location};");
            
            //Last TimeStamp
            DateTime lastTimeStamp = data.Where(x => x.DeviceId == device.DeviceId).Max(x => x.Timestamp);
            
            //Last 4 hours of data
            var last4Hours = data.Where(x => x.DeviceId == device.DeviceId && x.Timestamp >= lastTimeStamp.AddHours(-4)).ToList();
            
            //Average Rainfall 
            var avgRainfall = last4Hours.Average(x => x.DataValue);
            
            //Trend Increasing or Decreasing average of first 2 values vs last 2 values
            var trend = last4Hours.Count() > 3 ? (last4Hours.Take(2).Average(x => x.DataValue) < last4Hours.Skip(2).Average(x => x.DataValue) ? "Increasing" : "Decreasing") : "N/A";
            
            string rainfallStatus = avgRainfall switch
            {
                < 10 => "Green",
                < 15 => "Amber",
                _ => "Red"
            };
            
            // Rainfall status to red if any value is greater than 30
            if (last4Hours.Any(x => x.DataValue > 30))
            {
                rainfallStatus = "Red";
            }
            
            // Set the console color based on the rainfallStatus
            Console.ForegroundColor = rainfallStatus switch
            {
                "Green" => ConsoleColor.Green,
                "Amber" => ConsoleColor.Yellow,
                _ => ConsoleColor.Red
            };

            Console.WriteLine($"Average rainfall {avgRainfall:F2}mm; Status: {rainfallStatus}; Trend: {trend};");
            
            Console.ResetColor();
            
        }
    }
    
    ///<summary>
    ///Read Device CSV file and parse it into records using CsvHelper (dotnet add package CsvHelper)
    ///</summary>
    ///<param name="filePath">CSV file path</param>
    ///<returns>List of Device records</returns>
    private static List<Device> ReadDeviceCsv(string filePath)
    {
        // Read the CSV file and parse it into records
        try
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            
            // Return the records as a list
            return csv.GetRecords<Device>().ToList();
        }
        catch (FileNotFoundException ex)
        {
            // Handle file not found exception
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            // Handle any other exceptions
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.ResetColor();
        }
        Environment.Exit(1); // Exit the program with an error code
        return [];
    }
    
        
    ///<summary>
    ///Read Data CSV file and parse it into records using CsvHelper (dotnet add package CsvHelper)
    ///</summary>
    ///<param name="filePath">CSV file path</param>
    ///<returns>List of Data records</returns>
    private static List<Data> ReadDataCsv(string filePath)
    {
        // Read the CSV file and parse it into records
        try
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            
            // Return the records as a list
            return csv.GetRecords<Data>().ToList();
        }
        catch (FileNotFoundException ex)
        {
            // Handle file not found exception
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            // Handle any other exceptions
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        Environment.Exit(1); // Exit the program with an error code
        return [];
    }
    
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
}