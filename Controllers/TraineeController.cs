using Role_based.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Role_based.Controllers
{
    [Authorize]
    public class TraineeController : Controller
    {
        MVCDatabaseEntities mvcdb = new MVCDatabaseEntities();
        public ActionResult Index(int? DomainID)
        {
            List<Trainee> trainee = mvcdb.Trainees.Where(tr => tr.DomainID == DomainID).ToList();
            return View(trainee);
        }

        public ActionResult View0()
        {
            List<Trainee> trainee = mvcdb.Trainees.ToList();
            return View(trainee);
        }
        public ActionResult View1()
        {
            List<Trainee> trainee = mvcdb.Trainees.ToList();
            return View(trainee);
        }

        public ActionResult View2()
        {
            List<Trainee> trainee = mvcdb.Trainees.ToList();
            return View(trainee);
        }
        public ActionResult Create()
        {
            List<Training> training = mvcdb.Trainings.ToList();
            ViewBag.Trainings = new SelectList(training, "DomainID", "Domain");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase TImage, [Bind(Include = "Tname, Tage, Tcity, DomainID")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                byte[] pict;

                using (var reader = new BinaryReader(TImage.InputStream))
                {
                    pict = reader.ReadBytes(TImage.ContentLength);
                }
                trainee.TImage = pict;
                mvcdb.Trainees.Add(trainee);
                mvcdb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            Trainee trainee = mvcdb.Trainees.Find(id);
            return View(trainee);
        }

        public ActionResult Edit(int? id)
        {
            Trainee trainee = mvcdb.Trainees.Find(id);
            List<Training> training = mvcdb.Trainings.ToList();
            ViewBag.Trainings = new SelectList(training, "DomainID", "Domain");
            return View(trainee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase TImage, [Bind(Include = "TraineeID,Tname,Tage,Tcity,DomainID")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                byte[] edpict;

                using (var reader = new BinaryReader(TImage.InputStream))
                {
                    edpict = reader.ReadBytes(TImage.ContentLength);
                }
                trainee.TImage = edpict;
                mvcdb.Entry(trainee).State = System.Data.Entity.EntityState.Modified;
                mvcdb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            Trainee trainee = mvcdb.Trainees.Find(id);
            return View(trainee);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainee trainee = mvcdb.Trainees.Find(id);
            mvcdb.Trainees.Remove(trainee);
            mvcdb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}