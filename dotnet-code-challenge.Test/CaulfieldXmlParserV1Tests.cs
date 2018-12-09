using System.IO;
using Xunit;

namespace dotnet_code_challenge.Test
{
    public class CaulfieldXmlParserV1Tests
    {
        [Fact]
        public void VerifyParseHorseDataWithValidFeedDataFile()
        {
            var testFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"TestFeedDataFiles\Caulfield_Race1.xml");
            Assert.True(File.Exists(testFilePath));

            var xmlParser = new CaulfieldXmlParserV1();
            var horses = xmlParser.ParseHorseData(testFilePath);

            Assert.Equal(2, horses.Count);
            Assert.Contains(horses, h => h.Name == "Advancing" && h.Price == 4.2);
            Assert.Contains(horses, h => h.Name == "Coronel" && h.Price == 12);
        }



    }
}
