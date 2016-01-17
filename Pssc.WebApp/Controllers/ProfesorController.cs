using Pssc.Database;
using Pssc.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pssc.WebApp.Controllers
{
    public class ProfesorController : Controller
    {
        protected readonly PsscEntities _context;
        public ProfesorController(PsscEntities context)
        {
            _context = context;
        }
        // GET: Profesor
        public ActionResult Index()
        {
            var list = _context.Student.ToList().Select(x => new StudentViewModel
            {
                FirstName=x.StudentFirstName,
                LastName=x.StudentLastName,
                Faculty=x.Faculty,
                Id=x.Id
            });
            return View(list);
        }

        public ActionResult Grade(long id)
        {
            Student stud = null;
            using (var tran = _context.Database.BeginTransaction())
            {
                stud = _context.Student.SingleOrDefault(x => x.Id == id);
                tran.Commit();
            }
            return View(new StudentViewModel() {FirstName=stud.StudentFirstName,LastName=stud.StudentLastName,Faculty=stud.Faculty,Id=stud.Id });
        }

        public ActionResult GradeAdd(StudentViewModel model)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                var stud = _context.Student.SingleOrDefault(x => x.Id == model.Id);
                var grade = new StudentGrades() { Grade = model.Grade };
                stud.StudentGrades.Add(grade);
                _context.SaveChanges();
                tran.Commit();
            }
            return RedirectToAction("Index","Profesor");
        }

    }
}
