using Pssc.Database;
using Pssc.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pssc.WebApp.Controllers
{
    public class AdministratorController : Controller
    {
        public readonly PsscEntities _context;
        public AdministratorController(PsscEntities context)
        {
            _context = context;
        }
        // GET: Administrator
        public ActionResult Index()
        {
            List<DormListRowViewModel> list = new List<DormListRowViewModel>();
            using (var tran = _context.Database.BeginTransaction())
            {
                var myList = _context.DormList.ToList().Select(
                    x=>new DormListRowViewModel
                    {
                        FirstName=x.Student.StudentFirstName,
                        LastName=x.Student.StudentLastName,
                        Average=x.Average
                    });
                list.AddRange(myList);
                tran.Commit();
            }

                return View(list);
        }
    }
}