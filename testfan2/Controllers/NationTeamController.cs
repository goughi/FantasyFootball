using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testfan2.Models;



namespace testfan2.Controllers
{
    public class NationTeamController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NationTeam
        public ActionResult Index()
        {
            return View(db.NationTeams.ToList());
        }

        // GET: NationTeam/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationTeam nationTeam = db.NationTeams.Find(id);
            if (nationTeam == null)
            {
                return HttpNotFound();
            }
            return View(nationTeam);
        }



        // GET: NationTeam/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NationTeam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NationCode,Nation,TeamGoalWeight")] NationTeam nationTeam)
        {
            if (ModelState.IsValid)
            {
                db.NationTeams.Add(nationTeam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nationTeam);
        }

        // GET: NationTeam/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationTeam nationTeam = db.NationTeams.Find(id);
            if (nationTeam == null)
            {
                return HttpNotFound();
            }
            return View(nationTeam);
        }

        // POST: NationTeam/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NationCode,Nation,TeamGoalWeight")] NationTeam nationTeam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nationTeam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nationTeam);
        }

        // GET: NationTeam/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationTeam nationTeam = db.NationTeams.Find(id);
            if (nationTeam == null)
            {
                return HttpNotFound();
            }
            return View(nationTeam);
        }

        // POST: NationTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NationTeam nationTeam = db.NationTeams.Find(id);
            db.NationTeams.Remove(nationTeam);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
