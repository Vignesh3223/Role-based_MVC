using Role_based.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Role_based.Controllers
{
    [Authorize]
    public class TrainingController : Controller
    {
        MVCDatabaseEntities mvcdb = new MVCDatabaseEntities();
        // GET: Training
        public ActionResult Index()
        {
            List<Training> training = mvcdb.Trainings.ToList();
            return View(training);
        }

        public ActionResult Training1()
        {
            List<Training> training = mvcdb.Trainings.ToList();
            return View(training);
        }

        public ActionResult Training2()
        {
            List<Training> training = mvcdb.Trainings.ToList();
            return View(training);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DomainID,Domain,Trainer")] Training training)
        {
            if (ModelState.IsValid)
            {
                mvcdb.Trainings.Add(training);
                mvcdb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            Training training = mvcdb.Trainings.Find(id);
            return View(training);
        }

        public ActionResult Edit(int? id)
        {
            Training training = mvcdb.Trainings.Find(id);
            return View(training);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DomainID,Domain,Trainer")] Training training)
        {
            if (ModelState.IsValid)
            {
                mvcdb.Entry(training).State = System.Data.Entity.EntityState.Modified;
                mvcdb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            Training training = mvcdb.Trainings.Find(id);
            return View(training);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Training training = mvcdb.Trainings.Find(id);
            mvcdb.Trainings.Remove(training);
            mvcdb.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}