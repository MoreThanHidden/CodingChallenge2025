using System.Globalization;
using CsvHelper;

namespace CodingChallenge2025;

/// <summary>
///  Data Reader Interface
/// </summary>
public interface IDataReader
{
    List<Device> ReadDevices();
    List<Data> ReadData();
}

/// <summary>
///  CSV Data Reader Implementation
/// </summary>
public class CsvDataReader : IDataReader
{
    //  Directory path for CSV files
    private readonly string _directoryPath;

    /// <summary>
    ///  Constructor for Csv Data Reader
    /// </summary>
    /// <param name="directoryPath">Path containing CSV Files</param>
    public CsvDataReader(string directoryPath)
    {
        _directoryPath = directoryPath;
    }

    /// <summary>
    ///  Reads devices from the CSV file
    /// </summary>
    /// <returns>List of Devices from the CSV</returns>
    public List<Device> ReadDevices()
    {
        string filePath = $"{_directoryPath}\\Devices.csv";
        return ReadCsv<Device>(filePath);
    }

    /// <summary>
    /// Reads data from the CSV files
    /// </summary>
    /// <returns>List of Data from the CSV</returns>
    public List<Data> ReadData()
    {
        var data = new List<Data>();
        
        //find all files in the directory that match the pattern "Data*.csv"
        var files = Directory.GetFiles(_directoryPath, "Data*.csv");
        
        //check if any files were found
        if (files.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No Data*.csv files found in the directory: {_directoryPath}");
            Console.ResetColor();
            Environment.Exit(1);
        }
        
        //read each file and add the records to the data list
        foreach (var file in files)
        {
            data.AddRange(ReadCsv<Data>(file));
        }
        
        return data;
    }

    /// <summary>
    ///  Reads a CSV file and parses it into records using CsvHelper
    /// </summary>
    /// <param name="filePath">Path to the CSV File</param>
    /// <returns>List of Data from the CSV</returns>
    private static List<T> ReadCsv<T>(string filePath)
    {
        try
        {
            // Read the CSV file and return the Objects as a list
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<T>().ToList();
        }
        catch (Exception ex)
        {
            // Handle file not found exception or any other exceptions
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error reading file {filePath}: {ex.Message}");
            Console.ResetColor();
            Environment.Exit(1);
            return [];
        }
    }
}