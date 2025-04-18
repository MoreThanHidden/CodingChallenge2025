namespace CodingChallenge2025.Tests
{
    public class CsvDataReaderTests
    {
        [Fact]
        public void ReadDevices_ShouldReturnCorrectDevices()
        {
            // Arrange
            var testDirectory = "TestData";
            Directory.CreateDirectory(testDirectory);
            File.WriteAllText(Path.Combine(testDirectory, "Devices.csv"), "Device ID,Device Name,Location\n1,Device1,Location1");

            var dataReader = new CsvDataReader(testDirectory);

            // Act
            var devices = dataReader.ReadDevices();

            // Assert
            Assert.Single(devices);
            Assert.Equal(1, devices[0].DeviceId);
            Assert.Equal("Device1", devices[0].DeviceName);
            Assert.Equal("Location1", devices[0].Location);

            // Cleanup
            Directory.Delete(testDirectory, true);
        }
    }
}