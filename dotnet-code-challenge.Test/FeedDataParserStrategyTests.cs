using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using System.IO;
using System;

namespace dotnet_code_challenge.Test
{
    public class FeedDataParserStrategyTests
    {
        [Fact]
        public void ValidateProcessFilesInDirectoryWithValidPath()
        {
            var caulfieldParserMock = new Mock<IFeedDataParser>();
            caulfieldParserMock.Setup(x => x.ParseHorseData(It.IsAny<string>())).Returns(new List<Horse> { new Horse { Name = "Test1", Price = 1 } });
            var wolferhamptonParserMock = new Mock<IFeedDataParser>();
            wolferhamptonParserMock.Setup(x => x.ParseHorseData(It.IsAny<string>())).Returns(new List<Horse> { new Horse { Name = "Test2", Price = 2 } });

            var strategy = new FeedDataParserStrategy(caulfieldParserMock.Object, wolferhamptonParserMock.Object);
            var inputFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFeedDataFiles");

            Assert.True(Directory.Exists(inputFolderPath));
            Assert.True(Directory.EnumerateFiles(inputFolderPath, "*.xml").Any());
            Assert.True(Directory.EnumerateFiles(inputFolderPath, "*.json").Any());

            var horses = strategy.ProcessFilesInDirectory(inputFolderPath);

            Assert.Equal("Test1", horses.First().Name);
        }

        [Fact]
        public void ValidateProcessFilesInDirectoryWithInvalidPath()
        {
            var caulfieldParserMock = new Mock<IFeedDataParser>();
            var wolferhamptonParserMock = new Mock<IFeedDataParser>();

            var strategy = new FeedDataParserStrategy(caulfieldParserMock.Object, wolferhamptonParserMock.Object);

            Assert.Throws<ArgumentException>(() => strategy.ProcessFilesInDirectory("sometestjkhkasf"));
        }
    }
}
