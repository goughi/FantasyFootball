using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using testfan2.Models;
using testfan2.ViewModels;

namespace testfan2.Controllers
{
    public class PlayerRoundStatController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

        public ActionResult ShowTeamData(int? fixtureId)
        {
            if (fixtureId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<RoundTeamViewModel> playerStats = new List<RoundTeamViewModel>();
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {

                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;

                    if (userIdValue != null)
                    {
                        var fixtures = db.Fixtures.ToList().Where(f => f.gamePlayed == true && f.RoundStage == RoundStage.FirstRound);
                        var team = db.FantasyTeams.Include(t => t.Players).Where(t => t.UserId == userIdValue).FirstOrDefault();
                        if (team != null)
                        {

                                    foreach (var player in team.Players)
                                    {
                                    var stat = db.PlayerRoundStats.ToList().Where(s => s.FixtureId == fixtureId && s.PlayerID == player.PlayerID).SingleOrDefault();
                                    if (stat != null)
                                        playerStats.Add(new RoundTeamViewModel
                                        {
                                            CleanSheet = stat.CleanSheet, YellowCarded = stat.YellowCarded, RedCarded = stat.RedCarded,
                                            goalScored = stat.goalScored, GoalsConceded = stat.GoalsConceded, IsWin = stat.IsWin, ManOfTheMatch = stat.ManOfTheMatch, MinutesPlayed = stat.MinutesPlayed,
                                            PlayerRoundStatID = stat.PlayerRoundStatID, PlayerName = player.PlayerFirstname + ". " + player.PlayerSurname, playerTotal = stat.TotalPoints
                                        });
                                     
                                    }
                                   
                                    return View(playerStats.ToList());

                        }
                    }
                  
                }
            }
         
                TempData["Error"] = "Please log in to find your team";
               return RedirectToAction("Index", "Fixture");
         
            
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
