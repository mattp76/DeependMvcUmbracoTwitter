using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web;
using Umbraco;
using Umbraco.Core;
using Umbraco7.Models;
using Umbraco7.Services;
using System.Configuration;

namespace Umbraco7.Controllers
{
    public class HomePageController : BaseController<TwitterLatestTweetsModel>
    {
  
        public ActionResult Index()
        {

            var root = getRootNode();

            if (root.HasProperty("twitterAccountNames"))
            {
                if (root.HasValue("twitterAccountNames"))
                {
                    Model.TwitterAccountNames = new string[] { root.GetPropertyValue<String>("twitterAccountNames") };
                }
            }

            if (root.HasProperty("twitterMaxItems"))
            {
                if (root.HasValue("twitterMaxItems"))
                {
                    Model.MaxTweetCount = int.Parse(root.GetPropertyValue<String>("twitterMaxItems"));
                }
            }

            if (root.HasProperty("contentTitle"))
            {
                if (root.HasValue("contentTitle"))
                {
                    Model.ContentTitle = root.GetPropertyValue<String>("contentTitle");
                }
            }


            var UmbracoTwitterService = new UmbracoTwitterService(Model, root);

            return View(Model);
        }
    }
}