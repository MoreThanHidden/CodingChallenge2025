namespace CodingChallenge2025.Tests
{
    public class ConsoleOutputHandlerTests
    {
        [Fact]
        public void DisplayResult_ShouldWriteCorrectOutputToConsole()
        {
            // Arrange
            var device = new Device { DeviceId = 1, DeviceName = "Device1", Location = "Location1" };
            var result = new RainfallResult(12.34, "Increasing", "Green");
            var outputHandler = new ConsoleOutputHandler();

            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            outputHandler.DisplayResult(device, result);

            // Assert
            var output = consoleOutput.ToString();
            Assert.Contains("DeviceId: 1; DeviceName: Device1; Location: Location1;", output);
            Assert.Contains("Average rainfall 12.34mm; Status: Green; Trend: Increasing;", output);
        }
    }
}