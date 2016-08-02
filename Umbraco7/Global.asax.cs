using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Security;
using System.Web.SessionState;
using Umbraco.Web;

namespace Umbraco7
{

    public class MvcApplication : UmbracoApplication
    {
        protected override void OnApplicationStarted(object sender, EventArgs e)
        {
            //base.OnApplicationStarted(sender, e);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

    }


 
}