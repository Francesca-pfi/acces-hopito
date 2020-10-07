﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hopito.Models;

namespace Hopito.Controllers
{
    public class UserController : Controller
    {
        private HopitoDBEntities DB = new HopitoDBEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            LoginView user = new LoginView();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginView user)
        {
            return RedirectToAction("Index");
        }
        public ActionResult WaitingRoom()
        {
            Patient patient = new Patient();
            return View(patient);
        }
    }
}