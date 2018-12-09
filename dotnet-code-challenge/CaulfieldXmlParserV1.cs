using System;
using System.Collections.Generic;
using System.Xml;

namespace dotnet_code_challenge
{
    /// <summary>
    /// Class responsible for parcing horse data from the Caulfield XML file v1
    /// </summary>
    public class CaulfieldXmlParserV1 : IFeedDataParser
    {
        #region Private Properties

        //TODO: These XPaths shall be read from the config file sucha a way that, 
        //minor changes to the XML can be supported by updating the config file. -- Data Driven
        private static string horseNameXPath = @"//meeting/races/race/horses/horse/@name";
        private static string horseNumberXPath = "//meeting/races/race/horses/horse[@name=\"{0}\"]/number";
        private static string horsePriceXPath = "//meeting/races/race/prices/price/horses/horse[@number=\"{0}\"]/@Price";

        #endregion

        /// <summary>
        /// Method to prase the horse data from the feed data
        /// </summary>
        /// <param name="feedDataFilePath">Caulfield XML file path</param>
        /// <returns>list of horses with their names and prices</returns>
        public IList<Horse> ParseHorseData(string feedDataFilePath)
        {
            var horses = new List<Horse>();

            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(feedDataFilePath);

                var names = document.SelectNodes(horseNameXPath);
                double price = 0;
                var number = string.Empty;
                var priceInString = string.Empty;
                foreach (XmlNode name in names)
                {
                    var horse = new Horse { Name = name.InnerText };
                    number = document.SelectSingleNode(string.Format(horseNumberXPath, horse.Name)).InnerText;
                    priceInString = document.SelectSingleNode(string.Format(horsePriceXPath, number)).InnerText;
                    if (double.TryParse(priceInString, out price))
                    {
                        horse.Price = price;
                    }
                    else
                    {
                        //Could not parse the price from the XML not a valid double value
                        //TODO: Log the details and update the handling mechanism if required

                        throw new FeedDataParsingException($"Invalid price for the horse {horse.Name}", feedDataFilePath);
                    }
                    horses.Add(horse);
                }
            }
            catch (Exception ex)
            {
                //TODO: Log ERROR the details of the XML Exception and update the handling 
                //mechanism if requied 

                throw new FeedDataParsingException(ex.Message, feedDataFilePath, ex);
            }
            return horses;
        }
    }
}
