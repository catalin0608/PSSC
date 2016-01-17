using Pssc.Database;
using Pssc.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pssc.WebApp.Controllers
{
    public class LoginController : Controller
    {
        protected readonly PsscEntities _context;
        public LoginController(PsscEntities context)
        {
            _context = context;
        }
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                LoginT user = null;
                using (var transaction=_context.Database.BeginTransaction())
                {
                    user = _context.LoginT.SingleOrDefault(x=>x.UserName==model.UserName && x.UserPassword==model.UserPassword);
                    transaction.Commit();
                }
                var role = (user!=null)? user.UserRole :0;
                switch(role){
                    case 1:
                        return RedirectToAction("Index", "Profesor");
                    case 2:
                        return RedirectToAction("Index", "Secretariat");
                    case 3:
                        return RedirectToAction("Index", "Administrator");
                }
            }
            return RedirectToAction("Login", "Login");
        }      
    }
}
