using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco;
using Umbraco.Core;
using umbraco.NodeFactory;
using Umbraco.Web.Mvc;
using Umbraco.Web;
using Umbraco.Core.Models;

namespace Umbraco7.Controllers
{
    public class BaseController<T> : RenderMvcController where T : new()
    { 
        protected T Model { get; set; }
        private IPublishedContent _rootNode;

        protected BaseController()
        {
            Model = new T();
        }

        public IPublishedContent getRootNode()
        {
            return CurrentPage.AncestorOrSelf(1);
        }
    }
}