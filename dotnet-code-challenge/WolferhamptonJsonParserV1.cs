using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace dotnet_code_challenge
{
    public class WolferhamptonJsonParserV1 : IFeedDataParser
    {

        //TODO: These XPaths shall be read from the config file sucha a way that, 
        //minor changes to the XML can be supported by updating the config file. -- Data Driven
        private static string selectionsPath = "..Selections[*]";
        private static string tagsNamePath = ".Tags.name";
        private static string pricePath = ".Price";


        /// <summary>
        /// Parse JSON file
        /// </summary>
        /// <param name="feedDataFilePath">feed data file path</param>
        /// <returns>list of horses with their names and prices</returns>
        public IList<Horse> ParseHorseData(string feedDataFilePath)
        {
            var horses = new List<Horse>();
            try
            {
                using (StreamReader r = new StreamReader(feedDataFilePath))
                {
                    var json = r.ReadToEnd();
                    var jobj = JObject.Parse(json);
                    var selections = jobj.SelectTokens(selectionsPath);
                    foreach (JToken selection in selections)
                    {
                        var name = (string)selection.SelectToken(tagsNamePath);
                        double price = 0;
                        if(double.TryParse(selection.SelectToken(pricePath).ToString(), out price))
                        {
                            horses.Add(new Horse { Name = name, Price = price });
                        }
                        else
                        {
                            //LOG the details
                            throw new FeedDataParsingException($"Invalid price for the horse {name}", feedDataFilePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO Log exception 
                throw new FeedDataParsingException(ex.Message, feedDataFilePath, ex);
            }
            return horses;
        }
    }
}
