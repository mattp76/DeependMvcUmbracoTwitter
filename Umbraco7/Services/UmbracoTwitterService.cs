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
using Umbraco.Web;

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

        public UmbracoTwitterService(TwitterLatestTweetsModel model, IPublishedContent root)
        {

            if (root.HasProperty("twitterConsumerKey"))
            {
                if (root.HasValue("twitterConsumerKey"))
                {
                    TWITTER_CONSUMER_KEY = root.GetPropertyValue<String>("twitterConsumerKey");
                }
            }


            if (root.HasProperty("twitterConsumerSecret"))
            {
                if (root.HasValue("twitterConsumerSecret"))
                {
                    TWITTER_CONSUMER_SECRET = root.GetPropertyValue<String>("twitterConsumerSecret");
                }
            }

            if (root.HasProperty("twitterAccessToken"))
            {
                if (root.HasValue("twitterAccessToken"))
                {
                    TWITTER_ACCESS_TOKEN = root.GetPropertyValue<String>("twitterAccessToken");
                }
            }

            if (root.HasProperty("twitterAccessSecret"))
            {
                if (root.HasValue("twitterAccessSecret"))
                {
                    TWITTER_ACCESS_SECRET = root.GetPropertyValue<String>("twitterAccessSecret");
                }
            }

            if (root.HasProperty("twitterCacheDuration"))
            {
                if (root.HasValue("twitterCacheDuration"))
                {
                    double number;
                    string value;
                    value = root.GetPropertyValue<String>("twitterCacheDuration");

                    if (Double.TryParse(value, out number))
                        CACHE_DURATION_MINUTES = number; 
                }
            }


            model.Tweets = GetLatestTweets(model);
        }


        /// <summary>
        /// Gets the latest tweets.
        /// </summary>
        /// <param name="accountNames">The account names.</param>
        /// <param name="tweetCount">The tweet count.</param>
        /// <returns>
        ///     The collection of different user tweets
        /// </returns>
        public Dictionary<string, string[]> GetLatestTweets(TwitterLatestTweetsModel model)
        {
            var collection = new Dictionary<string, string[]>();

            if (model.TwitterAccountNames == null || model.TwitterAccountNames.Length == 0) return collection;

            foreach (var accountName in model.TwitterAccountNames)
            {
                var tweets = GetLatestTweets(accountName, model.MaxTweetCount, model);

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



        public string[] GetLatestTweets(string accountName, int tweetCount, TwitterLatestTweetsModel model)
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
                    model.Error = e.Message;
                }
            }
            else
            {
                return (string[])cachedTweets;
            }

            return null;
        }
    }
}