using System;
using System.IO;

namespace dotnet_code_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayProcessingDetails();
        }

        /// <summary>
        /// Show the processing details in the console.
        /// </summary>
        private static void DisplayProcessingDetails()
        {
            Console.WriteLine("Start processing the files in the FeedData folder");

            var parserStrategy = new FeedDataParserStrategy(new CaulfieldXmlParserV1(), new WolferhamptonJsonParserV1());

            try
            {
                var sortedHorses = parserStrategy.ProcessFilesInDirectory(Path.Combine(Directory.GetCurrentDirectory(), "FeedData"));

                foreach (var horse in sortedHorses)
                {
                    Console.WriteLine($"Name: {horse.Name}, Price: {horse.Price}");
                }

                Console.WriteLine("Press any key to exit");
                Console.Read();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception thrown: {ex.Message}");
                Console.WriteLine("Press any key to exit");
                Console.Read();
            }
        }
    }
}
