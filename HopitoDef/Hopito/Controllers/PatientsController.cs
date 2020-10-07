using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hopito.Models;

namespace Hopito.Controllers
{
    public class PatientsController : Controller
    {
        // GET: Patients
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WaitingRoom()
        {
            PatientView patient = new PatientView();
            return View(patient);
        }
    }
}