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
        List<Data> data1 = ReadDataCsv($"{dirPath}\\Data1.csv");
        List<Data> data2 = ReadDataCsv($"{dirPath}\\Data2.csv");
        
        // Iterate through the records and print the values
        foreach (var record in devices)
        {
            Console.WriteLine($"DeviceId: {record.DeviceId}; DeviceName: {record.DeviceName}; Location: {record.Location};");
        }
        
        foreach (var record in data1)
        {
            Console.WriteLine($"DeviceId: {record.DeviceId}; DataValue: {record.DataValue}; Timestamp: {record.Timestamp};");
        }
        
        foreach (var record in data2)
        {
            Console.WriteLine($"DeviceId: {record.DeviceId}; DataValue: {record.DataValue}; Timestamp: {record.Timestamp};");
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
        public required string DataValue { get; set; }
        [Name("Time")] 
        public required DateTime Timestamp { get; set; }
    }
}