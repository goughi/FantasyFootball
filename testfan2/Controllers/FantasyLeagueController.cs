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
    public class FantasyLeagueController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FantasyLeague
        public ActionResult Index()
        {
            return View(db.FantasyLeagues.ToList());
        }

        // GET: FantasyLeague/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyLeague fantasyLeague = db.FantasyLeagues.Find(id);
            if (fantasyLeague == null)
            {
                return HttpNotFound();
            }

            //var fixturesPlayed = db.Fixtures.ToList().Where(f => f.gamePlayed == true && f.RoundStage == RoundStage.FirstRound);
            //foreach(var team in fantasyLeague.FantasyTeams)
            //{
            //    var totalScore = 0;
            //    foreach(var fixture in fixturesPlayed)
            //    {
            //        foreach (var player in team.Players)
            //        {
            //            var stat = db.PlayerRoundStats.ToList().Where(s => s.FixtureId == fixture.FixtureId && s.PlayerID == player.PlayerID).SingleOrDefault();
            //            if (stat != null)
            //            {
            //                totalScore += stat.TotalPoints;
            //            }

            //        }
            //    }
                //team.FirstRoundScore = totalScore;
                //db.SaveChanges();
            //}
            return View(fantasyLeague);
        }

        public ActionResult RoundDetails(int? id, int? round)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyLeague fantasyLeague = db.FantasyLeagues.Find(id);
            if (fantasyLeague == null)
            {
                return HttpNotFound();
            }
            if (round == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                ViewBag.Round = round;
               // ViewBag.Title = "League Table for Round " + round + 1;
            }
      
            return View(fantasyLeague);
        }
        // GET: FantasyLeague/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: FantasyLeague/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FantasyLeagueId,FantasyLeagueName")] FantasyLeague fantasyLeague)
        {
            if (ModelState.IsValid)
            {
                db.FantasyLeagues.Add(fantasyLeague);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fantasyLeague);
        }

        // GET: FantasyLeague/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyLeague fantasyLeague = db.FantasyLeagues.Find(id);
            if (fantasyLeague == null)
            {
                return HttpNotFound();
            }
            return View(fantasyLeague);
        }

        // POST: FantasyLeague/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FantasyLeagueId,FantasyLeagueName")] FantasyLeague fantasyLeague)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fantasyLeague).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fantasyLeague);
        }

        // GET: FantasyLeague/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyLeague fantasyLeague = db.FantasyLeagues.Find(id);
            if (fantasyLeague == null)
            {
                return HttpNotFound();
            }
            return View(fantasyLeague);
        }

        // POST: FantasyLeague/Delete/5
      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FantasyLeague fantasyLeague = db.FantasyLeagues.Find(id);
            db.FantasyLeagues.Remove(fantasyLeague);
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
