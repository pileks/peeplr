﻿using System.Web;
using System.Web.Mvc;

namespace Peeplr.Web.Controllers
{
    public class ShellController : Controller
    {
        public ActionResult Shell()
        {
            return View();
        }
    }
}