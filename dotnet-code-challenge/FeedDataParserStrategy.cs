using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace dotnet_code_challenge
{
    public class FeedDataParserStrategy
    {
        private IFeedDataParser _caulfieldXmlParserV1;
        private IFeedDataParser _wolferhamptonJsonParserV1;

        //TODO: make this dictionary configurable - so that can be loaded from configuration
        private readonly Dictionary<string, IFeedDataParser> _extensionParserMapper; 

        public FeedDataParserStrategy(IFeedDataParser caulfieldXmlParserV1, IFeedDataParser wolferhamptonJsonParserV1)
        {
            _caulfieldXmlParserV1 = caulfieldXmlParserV1;
            _wolferhamptonJsonParserV1 = wolferhamptonJsonParserV1;

            //TODO: Finalize the logic for identifying the feed data file type
            //As of now assuming all XML files are from Caulfield race and all JSON files are from Wolferhampton race
            _extensionParserMapper = new Dictionary<string, IFeedDataParser>()
            {
                {".XML", _caulfieldXmlParserV1 },
                {".JSON", _wolferhamptonJsonParserV1 }
            };
        }

        /// <summary>
        /// Process the files in the directory and get the horse details sorted by Price
        /// </summary>
        /// <param name="inputFolderPath">input directory</param>
        /// <returns>List of horse details sorted by price</returns>
        /// <exception cref="FeedDataParsingException">Can throw FeedDataParsingException incase of invalid file</exception>
        /// <exception cref="ArgumentException">Can throw ArgumentException if the input is path doesn't exist</exception>
        public IList<Horse> ProcessFilesInDirectory(string inputFolderPath)
        {
            var horses = new List<Horse>();
            if (!Directory.Exists(inputFolderPath))
            {
                //TODO: Log the details
                Console.WriteLine("Input directory doesn't exist!");
                throw new ArgumentException("Invalid input folder path");
            }

            foreach (var filePath in Directory.EnumerateFiles(inputFolderPath, "*.*"))
            {
                var extension = Path.GetExtension(filePath).ToUpper();
                if (_extensionParserMapper.ContainsKey(extension))
                {
                    horses.AddRange(_extensionParserMapper.GetValueOrDefault(extension)?.ParseHorseData(filePath));
                }
            }

            //Sort the horses based on the Price
            return horses.OrderBy(h => h.Price).ToList();
        }
    }
}
