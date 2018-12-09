using System;

namespace dotnet_code_challenge
{
    public class FeedDataParsingException : Exception
    {
        //readonly property -- no set
        public string FeedDataFilePath { get; }

        #region Standard Constructors
        public FeedDataParsingException()
        {
        }

        public FeedDataParsingException(string message) : base(message)
        {
        }

        public FeedDataParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }
        #endregion

        #region Custom Constructors
        public FeedDataParsingException(string message, string feedDataFilePath, Exception innerException) : base(message, innerException)
        {
            FeedDataFilePath = feedDataFilePath;
        }

        public FeedDataParsingException(string message, string feedDataFilePath) : base(message)
        {
            FeedDataFilePath = feedDataFilePath;
        }
        #endregion
    }
}
