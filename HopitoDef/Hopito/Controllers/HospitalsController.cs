using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hopito.Models;

namespace Hopito.Controllers
{
    public class HospitalsController : Controller
    {
        // GET: Hospitals
        private HopitoDBEntities DB = new HopitoDBEntities();
        public ActionResult Index()
        {
            return View();
        }
    }
}