using System.Collections.Generic;

namespace dotnet_code_challenge
{
    /// <summary>
    /// Interface contains the details for methods to parse the data 
    /// from different types of Feed Data files
    /// </summary>
    public interface IFeedDataParser
    {
        /// <summary>
        /// Parse the file to get the list of horses
        /// </summary>
        /// <param name="feedDataFilePath">feed data file path</param>
        /// <returns>List of horses and prices in price ascending order</returns>
        /// <exception cref="FeedDataParsingException">Can throw FeedDataParsingException</exception>
        IList<Horse> ParseHorseData(string feedDataFilePath);
    }
}
