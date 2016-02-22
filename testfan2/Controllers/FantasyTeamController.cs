using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testfan2.Models;
using testfan2.ViewModels;

namespace testfan2.Controllers
{
    public class FantasyTeamController : Controller
    {
        private FantasyFootballContext db = new FantasyFootballContext();

        // GET: FantasyTeam
        public ActionResult Index()
        {
            var fantasyTeams = db.FantasyTeams.Include(f => f.Customer).Include(f => f.FantasyLeague);
            return View(fantasyTeams.ToList());
        }

        // GET: FantasyTeam/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyTeam fantasyTeam = db.FantasyTeams.Find(id);
            if (fantasyTeam == null)
            {
                return HttpNotFound();
            }
            return View(fantasyTeam);
        }

        // GET: FantasyTeam/Create
        public ActionResult Create(int? CustomerID)
        {
            if (CustomerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyTeam fantasyTeam = new FantasyTeam();
            fantasyTeam.Players = new List<Player>();
            fantasyTeam.CustomerId = db.Customers.Where(c=> c.CustomerID == CustomerID).Single().CustomerID;
            ViewBag.CustomerName = db.Customers.Where(c => c.CustomerID == CustomerID).Single().CustomerFirstName;
            ViewBag.FantasyLeagueID = new SelectList(db.FantasyLeagues, "FantasyLeagueId", "FantasyLeagueName");
            return View(fantasyTeam);
        }

        // POST: FantasyTeam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamID,TeamName,CustomerId,FantasyLeagueID")] FantasyTeam fantasyTeam)
        {
            if (ModelState.IsValid)
            {
                db.FantasyTeams.Add(fantasyTeam);
                db.SaveChanges();
                return RedirectToAction("BuildTeam");
            }

           // ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "CustomerSurname", fantasyTeam.CustomerId);
            ViewBag.FantasyLeagueID = new SelectList(db.FantasyLeagues, "FantasyLeagueId", "FantasyLeagueName", fantasyTeam.FantasyLeagueID);
            return View(fantasyTeam);
        }
        
        public ActionResult BuildTeam(int? teamId)
        {
            var ViewModel = new ViewGks();
            ViewModel.players = db.Players.Where(g => g.Position == Position.GoalKeeper).ToList();
            if (teamId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyTeam fantasyTeam = db.FantasyTeams.Find(teamId);
            if (fantasyTeam == null)
            {
                return HttpNotFound();
            }
            return View(fantasyTeam);
        }

        public ActionResult GoalkeeperSearch

        // GET: FantasyTeam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyTeam fantasyTeam = db.FantasyTeams.Find(id);
            if (fantasyTeam == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "CustomerSurname", fantasyTeam.CustomerId);
            ViewBag.FantasyLeagueID = new SelectList(db.FantasyLeagues, "FantasyLeagueId", "FantasyLeagueName", fantasyTeam.FantasyLeagueID);
            return View(fantasyTeam);
        }

        // POST: FantasyTeam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamID,TeamName,CustomerId,FantasyLeagueID")] FantasyTeam fantasyTeam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fantasyTeam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "CustomerSurname", fantasyTeam.CustomerId);
            ViewBag.FantasyLeagueID = new SelectList(db.FantasyLeagues, "FantasyLeagueId", "FantasyLeagueName", fantasyTeam.FantasyLeagueID);
            return View(fantasyTeam);
        }

        // GET: FantasyTeam/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyTeam fantasyTeam = db.FantasyTeams.Find(id);
            if (fantasyTeam == null)
            {
                return HttpNotFound();
            }
            return View(fantasyTeam);
        }

        // POST: FantasyTeam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FantasyTeam fantasyTeam = db.FantasyTeams.Find(id);
            db.FantasyTeams.Remove(fantasyTeam);
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
