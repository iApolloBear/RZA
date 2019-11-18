using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RZA.Controllers
{
    public class RZAController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Panic(string name, int ID = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["numTimes"] = ID;

            return View();
        }
    }
}