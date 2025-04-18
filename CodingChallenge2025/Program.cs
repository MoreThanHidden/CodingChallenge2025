namespace CodingChallenge2025;

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

        //read the directory path from command line arguments or use current directory
        string dirPath = args.Length > 0 ? args[0] : ".";
        
        // Set the console color to green for better visibility then reset it
        Console.ForegroundColor = ConsoleColor.Green;
        // Inform the user of the directory being used
        Console.WriteLine($"Reading Data from: {dirPath}\\");
        Console.ResetColor();
        
        // Dependency injection for better testability
        IDataReader dataReader = new CsvDataReader(dirPath);
        IDataProcessor dataProcessor = new RainfallDataProcessor();
        IOutputHandler outputHandler = new ConsoleOutputHandler();
        
        // Read devices and data
        var devices = dataReader.ReadDevices();
        var data = dataReader.ReadData();
        
        // Process and display results
        foreach (var device in devices)
        {
            var result = dataProcessor.ProcessData(device, data);
            outputHandler.DisplayResult(device, result);
        }
        
    }
    
}