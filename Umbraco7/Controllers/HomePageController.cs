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
          
            Model.TwitterAccountNames = new string[] { CurrentPage.GetPropertyValue<String>("twitterAccountNames")};
            Model.MaxTweetCount = int.Parse(CurrentPage.GetPropertyValue<String>("twitterMaxItems"));
            Model.ContentTitle = CurrentPage.GetPropertyValue<String>("contentTitle");
            
            var UmbracoTwitterService = new UmbracoTwitterService(Model, CurrentPage);

            return View(Model);
        }
    }
}