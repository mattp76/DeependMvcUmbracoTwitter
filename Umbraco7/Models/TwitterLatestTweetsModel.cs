using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco7.Models
{
    public class TwitterLatestTweetsModel
    {

        public string ContentTitle { get; set; }
        public string[] HashTags { get; set; }
        public string[] HashTagsAccountNames { get; set; }
        public int MaxTaggedTweetSearchCount { get; set; }
        public int MaxTweetCount { get; set; }
        public Dictionary<string, string[]> Tweets { get; set; }
        public string[] TwitterAccountNames { get; set; }
        public string Error { get; set; }

    }
}