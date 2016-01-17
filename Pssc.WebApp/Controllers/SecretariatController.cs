using Pssc.Database;
using Pssc.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pssc.WebApp.Controllers
{
    public class SecretariatController : Controller
    {
        protected readonly PsscEntities _context;
        public SecretariatController(PsscEntities context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            IEnumerable<Student> list = null;
            using (var tran=_context.Database.BeginTransaction())
            {
                list = _context.Student.ToList();
                tran.Commit();
            }
                return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel model)
        {
            if (ModelState.IsValid) {
                using (var tran = _context.Database.BeginTransaction())
                {
                    var student = new Student {
                        StudentFirstName = model.FirstName,
                        StudentLastName = model.LastName,
                        Faculty = model.Faculty
                    };
                    _context.Student.Add(student);
                    _context.SaveChanges();
                    tran.Commit();
                }
                return RedirectToAction("Index","Secretariat");
            }

            return View();
        }

        // GET: Secretariat/Edit/5
        public ActionResult Edit(long id)
        {
            Student stud = null;
            using (var tran=_context.Database.BeginTransaction())
            {
                stud = _context.Student.SingleOrDefault(x=>x.Id==id);
                tran.Commit();
            }
            var model = new StudentViewModel {
                FirstName=stud.StudentFirstName,
                LastName=stud.StudentLastName,
                Faculty=stud.Faculty,
                Id=stud.Id
            };
            return View(model);
        }

        // POST: Secretariat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel model)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                var stud = _context.Student.Single(x => x.Id==model.Id);
                stud.StudentFirstName = model.FirstName;
                stud.StudentLastName = model.LastName;
                stud.Faculty = model.Faculty;
                _context.SaveChanges();
                tran.Commit();
            }
            return RedirectToAction("Index","Secretariat");
        }

        [HttpGet]
        public ActionResult Delete(long id)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                var stud = _context.Student.Single(x => x.Id == id);
                _context.Student.Remove(stud);
                _context.SaveChanges();
                tran.Commit();
            }
            return RedirectToAction("Index", "Secretariat");
        }

        public ActionResult ComputeDormList()
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                var dormList = _context.Student.ToList().Select(x => new DormList
                {
                    Student = x,
                    Average = (int)x.StudentGrades.Select(z => z.Grade).Average()
                });
                _context.DormList.SqlQuery("delete from DormList;");
                _context.DormList.AddRange(dormList);
                _context.SaveChanges();
                tran.Commit();
            }

            return RedirectToAction("Index","Secretariat");
        }


        public ActionResult DormList()
        {
            List<DormListRowViewModel> list = new List<DormListRowViewModel>();
            using (var tran = _context.Database.BeginTransaction())
            {
                var myList = _context.DormList.ToList().Select(
                    x => new DormListRowViewModel
                    {
                        FirstName = x.Student.StudentFirstName,
                        LastName = x.Student.StudentLastName,
                        Average = x.Average
                    });
                list.AddRange(myList);
                tran.Commit();
            }

            return View(list);
        }
    }
}
