using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Mvc;

namespace Umbraco7.Controllers
{
    public class BaseController<T> : RenderMvcController where T : new()
    { 
        protected T Model { get; set; }


        protected BaseController()
        {
            Model = new T();
        }

    }
}