using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anchor.FA.Model;
using Anchor.FA.BLL.IBLL;

namespace Anchor.FA.Web.Controllers
{
    public class SharedController:Controller
    {
        //
        // GET: /Shared/

        public ActionResult SystemError()
        {
            return View();
        }

        public ActionResult AuthorError()
        {
            return View();
        }

    }
}
