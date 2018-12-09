using System.IO;
using Xunit;

namespace dotnet_code_challenge.Test
{
    public class WolferhamptonJsonParserV1Tests
    {
        [Fact]
        public void VerifyParseHorseDataWithValidFeedDataFile()
        {
            var testFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"TestFeedDataFiles\Wolferhampton_Race1.json");
            Assert.True(File.Exists(testFilePath));

            var jsonParser = new WolferhamptonJsonParserV1();
            var horses = jsonParser.ParseHorseData(testFilePath);

            Assert.Equal(2, horses.Count);
            Assert.Contains(horses, h => h.Name == "Toolatetodelegate" && h.Price == 10);
            Assert.Contains(horses, h => h.Name == "Fikhaar" && h.Price == 4.4);
        }

        [Fact]
        
        public void VerifyParseHorseDataWithInvalidFeedDataJsonFileFormat()
        {

        }
    }
}
