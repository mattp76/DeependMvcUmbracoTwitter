using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco7.Interfaces;
using TweetSharp;
using Umbraco7.Models;
using umbraco.NodeFactory;
using umbraco.MacroEngines;
using System.Configuration;
using Umbraco.Core.Models;

namespace Umbraco7.Services
{
    public class UmbracoTwitterService : IUmbracoTwitterService
    {

        private string TWITTER_CONSUMER_KEY;
        private string TWITTER_CONSUMER_SECRET;
        private string TWITTER_ACCESS_TOKEN;
        private string TWITTER_ACCESS_SECRET;
        private double CACHE_DURATION_MINUTES;

        private string CACHE_KEY_TWEETS = "TwitterLatestTweets.Tweets_{0}_{1}";

        public UmbracoTwitterService(TwitterLatestTweetsModel model, IPublishedContent content)
        {

            var node = new DynamicNode(content.Id);

            TWITTER_CONSUMER_KEY = node.GetPropertyValue("twitterConsumerKey");
            TWITTER_CONSUMER_SECRET = node.GetPropertyValue("twitterConsumerSecret");
            TWITTER_ACCESS_TOKEN = node.GetPropertyValue("twitterAccessToken");
            TWITTER_ACCESS_SECRET = node.GetPropertyValue("twitterAccessSecret");
            CACHE_DURATION_MINUTES = Convert.ToDouble(node.GetPropertyValue("twitterCacheDuration"));

            GetLatestTweets(model);
        }


        /// <summary>
        /// Gets the latest tweets.
        /// </summary>
        public void GetLatestTweets(TwitterLatestTweetsModel model)
        {
             model.Tweets = GetLatestTweets(model.TwitterAccountNames, model.MaxTweetCount);
        }

        /// <summary>
        /// Gets the latest tweets.
        /// </summary>
        /// <param name="accountNames">The account names.</param>
        /// <param name="tweetCount">The tweet count.</param>
        /// <returns>
        ///     The collection of different user tweets
        /// </returns>
        public Dictionary<string, string[]> GetLatestTweets(string[] accountNames, int tweetCount)
        {
            var collection = new Dictionary<string, string[]>();

            if (accountNames == null || accountNames.Length == 0) return collection;

            foreach (var accountName in accountNames)
            {
                var tweets = GetLatestTweets(accountName, tweetCount);

                if (!collection.ContainsKey(accountName))
                {
                    collection[accountName] = tweets;
                }
                else
                {
                    var tmp = new List<string>(collection[accountName]);

                    tmp.AddRange(tweets);

                    collection[accountName] = tmp.ToArray();
                }
            }

            return collection;
        }



        public string[] GetLatestTweets(string accountName, int tweetCount)
        {

            var cacheKey = String.Format(CACHE_KEY_TWEETS, accountName, tweetCount);
            var cachedTweets = Helpers.CacheHelper.Get<string[]>(cacheKey);

            TwitterService service = new TwitterService(TWITTER_CONSUMER_KEY, TWITTER_CONSUMER_SECRET);
            service.AuthenticateWith(TWITTER_ACCESS_TOKEN, TWITTER_ACCESS_SECRET);

            if (cachedTweets == null)
            {
                var query = accountName.Replace("@", String.Empty);
                SearchOptions searchOption = new SearchOptions { Q = query, Count = tweetCount };
                
                try
                {

                    var response = service.Search(searchOption);

                    if (response != null)
                    {

                        var taggedTweets = response.Statuses.Select(item => item.Text).ToArray();

                        if (taggedTweets != null && taggedTweets.Count() > 0)
                        {
                            cachedTweets = taggedTweets;

                            Helpers.CacheHelper.Set(cacheKey, cachedTweets, DateTimeOffset.MinValue.AddMinutes(CACHE_DURATION_MINUTES));
                            return (string[])cachedTweets;
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
            else
            {

                var test = "hello";
                return (string[])cachedTweets;
            }

            return null;
        }
    }
}