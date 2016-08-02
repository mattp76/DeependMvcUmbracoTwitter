using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco7.Models;

namespace Umbraco7.Interfaces
{
    public interface IUmbracoTwitterService
    {
        void GetLatestTweets(TwitterLatestTweetsModel model);
        Dictionary<string, string[]> GetLatestTweets(string[] accountNames, int tweetCount);
        string[] GetLatestTweets(string accountName, int tweetCount);
    }
}