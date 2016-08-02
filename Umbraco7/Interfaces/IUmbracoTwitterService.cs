using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco7.Models;

namespace Umbraco7.Interfaces
{
    public interface IUmbracoTwitterService
    {
        Dictionary<string, string[]> GetLatestTweets(TwitterLatestTweetsModel model);
        string[] GetLatestTweets(string accountName, int tweetCount, TwitterLatestTweetsModel model);
    }
}