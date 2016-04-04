using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testfan2.DAL;
using testfan2.Models;

namespace testfan2.Controllers
{
    public class PlayerRoundStatController : Controller
    {
        private FantasyFootballContext db = new FantasyFootballContext();

        // GET: PlayerRoundStat
        public ActionResult Index()
        {
            var playerRoundStats = db.PlayerRoundStats.Include(p => p.Fixture).Include(p => p.Player);
            return View(playerRoundStats.ToList());
        }

        // GET: PlayerRoundStat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerRoundStat playerRoundStat = db.PlayerRoundStats.Find(id);
            if (playerRoundStat == null)
            {
                return HttpNotFound();
            }
            return View(playerRoundStat);
        }

        // GET: PlayerRoundStat/Create
        public ActionResult Create()
        {
            ViewBag.FixtureId = new SelectList(db.Fixtures, "FixtureId", "HomeTeamNationCode");
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "PlayerSurname");
            return View();
        }

        // POST: PlayerRoundStat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerRoundStatID,FixtureId,PlayerID,MinutesPlayed,CleanSheet,GoalsConceded,goalScored,YellowCarded,RedCarded,IsWin,ManOfTheMatch")] PlayerRoundStat playerRoundStat)
        {
            if (ModelState.IsValid)
            {
                db.PlayerRoundStats.Add(playerRoundStat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FixtureId = new SelectList(db.Fixtures, "FixtureId", "HomeTeamNationCode", playerRoundStat.FixtureId);
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "PlayerSurname", playerRoundStat.PlayerID);
            return View(playerRoundStat);
        }

        // GET: PlayerRoundStat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerRoundStat playerRoundStat = db.PlayerRoundStats.Find(id);
            if (playerRoundStat == null)
            {
                return HttpNotFound();
            }
            ViewBag.FixtureId = new SelectList(db.Fixtures, "FixtureId", "HomeTeamNationCode", playerRoundStat.FixtureId);
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "PlayerSurname", playerRoundStat.PlayerID);
            return View(playerRoundStat);
        }

        // POST: PlayerRoundStat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerRoundStatID,FixtureId,PlayerID,MinutesPlayed,CleanSheet,GoalsConceded,goalScored,YellowCarded,RedCarded,IsWin,ManOfTheMatch")] PlayerRoundStat playerRoundStat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playerRoundStat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FixtureId = new SelectList(db.Fixtures, "FixtureId", "HomeTeamNationCode", playerRoundStat.FixtureId);
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "PlayerSurname", playerRoundStat.PlayerID);
            return View(playerRoundStat);
        }

        // GET: PlayerRoundStat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlayerRoundStat playerRoundStat = db.PlayerRoundStats.Find(id);
            if (playerRoundStat == null)
            {
                return HttpNotFound();
            }
            return View(playerRoundStat);
        }

        // POST: PlayerRoundStat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlayerRoundStat playerRoundStat = db.PlayerRoundStats.Find(id);
            db.PlayerRoundStats.Remove(playerRoundStat);
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
