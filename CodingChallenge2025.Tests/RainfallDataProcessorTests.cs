namespace CodingChallenge2025.Tests
{
    public class RainfallDataProcessorTests
    {
        [Fact]
        public void ProcessData_ShouldReturnCorrectResult()
        {
            // Arrange
            var device = new Device { DeviceId = 1, DeviceName = "Device1", Location = "Location1" };
            var data = new List<Data>()
            {
                new Data {DeviceId = 1,Timestamp = DateTime.Parse("2023-01-01T00:00:00"),DataValue =  10 },
                new Data { DeviceId = 1, Timestamp = DateTime.Parse("2023-01-01T01:00:00"), DataValue = 20 },
                new Data { DeviceId = 1, Timestamp = DateTime.Parse("2023-01-01T02:00:00"), DataValue = 15 },
                new Data { DeviceId = 1, Timestamp = DateTime.Parse("2023-01-01T02:30:00"), DataValue = 12 }
            };

            var processor = new RainfallDataProcessor();

            // Act
            var result = processor.ProcessData(device, data);

            // Assert
            Assert.Equal(14.25, result.AverageRainfall);
            Assert.Equal("Decreasing", result.Trend);
            Assert.Equal("Amber", result.Status);
        }
    }
}