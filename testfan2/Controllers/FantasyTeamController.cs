using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testfan2.Models;
using testfan2.ViewModels;
using testfan2.DAL;

namespace testfan2.Controllers
{
    public class FantasyTeamController : Controller
    {
        private FantasyFootballContext db = new FantasyFootballContext();

        // GET: FantasyTeam
        public ActionResult Index(int? teamId)
        {
            if (teamId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fantasyTeam = db.FantasyTeams.FirstOrDefault(t => t.TeamID == teamId);
            return View(fantasyTeam);
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
            var fantasyTeam = new FantasyTeam();
            fantasyTeam.Players = new List<Player>();
            // PopulateGoalkeepers(fantasyTeam);



            fantasyTeam.CustomerId = db.Customers.Where(c => c.CustomerID == CustomerID).Single().CustomerID;
            ViewBag.CustomerName = db.Customers.Where(c => c.CustomerID == CustomerID).Single().CustomerFirstName;
            ViewBag.FantasyLeagueID = new SelectList(db.FantasyLeagues, "FantasyLeagueId", "FantasyLeagueName");
            return View(fantasyTeam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamID,TeamName,CustomerId,FantasyLeagueID")] FantasyTeam fantasyTeam)
        {

            if (ModelState.IsValid)
            {
                db.FantasyTeams.Add(fantasyTeam);
                db.SaveChanges();
                return RedirectToAction("AddTeam", new { TeamID = fantasyTeam.TeamID });
            }
            ViewBag.FantasyLeagueID = new SelectList(db.FantasyLeagues, "FantasyLeagueId", "FantasyLeagueName", fantasyTeam.FantasyLeagueID);
            return View(fantasyTeam);
        }
        // POST: FantasyTeam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult AddTeam(int? TeamID)
        {
            if (TeamID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fantasyTeam = db.FantasyTeams.Include(p => p.Players).Where(t => t.TeamID == TeamID).Single();
            // fantasyTeam.Players = new List<Player>();
            PopulateGoalkeepers(fantasyTeam);
            if (fantasyTeam == null)
            {
                return HttpNotFound();
            }
            return View(fantasyTeam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTeam(int? TeamID, string[] def, string[] mid, string[]fwd, string[]gk)
        {
            if (TeamID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var teamToAddPlayers = db.FantasyTeams
               .Include(i => i.Players)
               .Where(i => i.TeamID == TeamID)
               .Single();

            if (TryUpdateModel(teamToAddPlayers, "",
    new string[] { "TeamName" }))
            {
                try {

                    //UpdateFantasyPlayers(mid, teamToAddPlayers);
                    //UpdateFantasyPlayers(fwd, teamToAddPlayers);
                    //UpdateFantasyPlayers(gk, teamToAddPlayers);
                    //db.SaveChanges();
                    if (def == null && mid == null && fwd == null && gk == null)
                    {
                        teamToAddPlayers.Players = new List<Player>();
                    }
                    var midHS = new HashSet<string>(mid);
                    var defHS = new HashSet<string>(def);
                    var fwdHS = new HashSet<string>(fwd);
                    var gkHS = new HashSet<string>(gk);
                    var fantasyPlayers = new HashSet<int>
                        (teamToAddPlayers.Players.Select(p => p.PlayerID));
                    foreach (var player in db.Players)
                    {
                        if (defHS.Contains(player.PlayerID.ToString())||midHS.Contains(player.PlayerID.ToString())|| fwdHS.Contains(player.PlayerID.ToString()) || gkHS.Contains(player.PlayerID.ToString()))
                        {
                            if (!fantasyPlayers.Contains(player.PlayerID))
                            {
                                teamToAddPlayers.Players.Add(player);
                            }
                        }
                        else
                        {
                            if (fantasyPlayers.Contains(player.PlayerID))
                            {
                                teamToAddPlayers.Players.Remove(player);
                            }
                        }
                    }
                    db.SaveChanges();
                    
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }

                //foreach (var player in def)
                //{
                //   var playerToAdd = db.Players.Find(int.Parse(player));
                //    fantasyTeam.Players.Add(playerToAdd);
                //  //  fantasyTeam.AddPlayer(playerToAdd);

                //}
            }
            return RedirectToAction("Index", new { TeamID = teamToAddPlayers.TeamID });
            //PopulateGoalkeepers(teamToAddPlayers);
            //return View(teamToAddPlayers);
        }
        
    

            
         

        // GET: FantasyTeam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyTeam fantasyTeam = db.FantasyTeams.Include(i => i.Players).Where(i => i.TeamID == id).Single();
            PopulateGoalkeepers(fantasyTeam);

            if (fantasyTeam == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "CustomerSurname", fantasyTeam.CustomerId);
           // ViewBag.FantasyLeagueID = new SelectList(db.FantasyLeagues, "FantasyLeagueId", "FantasyLeagueName", fantasyTeam.FantasyLeagueID);
            return View(fantasyTeam);
        }

        private void PopulateGoalkeepers(FantasyTeam fantasyTeam)
        {
            var goalkeepers = db.Players.ToList().Where(p => p.Position == Position.GoalKeeper);
            var defenders = db.Players.ToList().Where(p => p.Position == Position.Defender);
            var midfielders = db.Players.ToList().Where(p => p.Position == Position.Midfielder);
            var forwards = db.Players.ToList().Where(p => p.Position == Position.Forward);
           
            var fantasyPlayers = new HashSet<int>(fantasyTeam.Players.Select(p => p.PlayerID));

            var viewModelDef = new List<ViewPlayers>();
            var viewModelMid = new List<ViewPlayers>();
            var viewModelGk = new List<ViewPlayers>();
            var viewModelFwd = new List<ViewPlayers>();
            foreach (var player in goalkeepers)
            {
                viewModelGk.Add(new ViewPlayers
                {
                    playerId = player.PlayerID,
                    playerName = player.PlayerFirstname + " " + player.PlayerSurname + " " +  player.NationCode + " " + player.Position + "         " + player.PlayerValue + " ",
                    PlayerValue = player.PlayerValue,
                    added = fantasyPlayers.Contains(player.PlayerID)
                });
            }
            foreach (var player in defenders)
            {
                viewModelDef.Add(new ViewPlayers
                {
                    playerId = player.PlayerID,
                    playerName = player.PlayerFirstname + " " + player.PlayerSurname + " " + player.NationCode + " " + player.Position + "        " + player.PlayerValue + " ",
                    PlayerValue = player.PlayerValue,
                    added = fantasyPlayers.Contains(player.PlayerID)
                });
            }
            foreach (var player in midfielders)
            {
                viewModelMid.Add(new ViewPlayers
                {
                    playerId = player.PlayerID, 
                    playerName = player.PlayerFirstname + " " + player.PlayerSurname + " " + player.NationCode + " " + player.Position + "       " + player.PlayerValue + " ",
                    PlayerValue = player.PlayerValue,
                    added = fantasyPlayers.Contains(player.PlayerID)
                });
            }
            foreach (var player in forwards)
            {
                viewModelFwd.Add(new ViewPlayers
                {
                    playerId = player.PlayerID,
                    playerName = player.PlayerFirstname + " " + player.PlayerSurname + " " + player.NationCode + " " + player.Position + "       " + player.PlayerValue + " ",
                    PlayerValue = player.PlayerValue,
                    added = fantasyPlayers.Contains(player.PlayerID)
                });
            }
            ViewBag.Forwards = viewModelFwd;
            ViewBag.Goalkeepers = viewModelGk;
            ViewBag.Defenders = viewModelDef;
            ViewBag.Midfielders = viewModelMid;
        }

        // POST: FantasyTeam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedPlayers)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyTeam fantasyTeamToUpdate = db.FantasyTeams.Include(i => i.Players).Where(i => i.TeamID == id).Single();

            if (TryUpdateModel(fantasyTeamToUpdate, "",
       new string[] { "TeamName" }))
            {
                try
                {
                  

                    UpdateFantasyPlayers(selectedPlayers, fantasyTeamToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index", new {teamId = fantasyTeamToUpdate.TeamID } );
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
                catch(ArgumentException e)
                {
                    e.Message.ToString();
                }
            }
            PopulateGoalkeepers(fantasyTeamToUpdate);
            return View(fantasyTeamToUpdate);
        }
        private void UpdateFantasyPlayers(string[] selectedPlayers, FantasyTeam fantasyTeamToUpdate)
        {
            if (selectedPlayers == null)
            {
                fantasyTeamToUpdate.Players = new List<Player>();
                return;
            }

            var selectedPlayersHS = new HashSet<string>(selectedPlayers);
            var fantasyPlayers = new HashSet<int>
                (fantasyTeamToUpdate.Players.Select(p => p.PlayerID));
            foreach (var player in db.Players)
            {
                try {
                    if (selectedPlayersHS.Contains(player.PlayerID.ToString()))
                    {
                        if (!fantasyPlayers.Contains(player.PlayerID))
                        {
                            fantasyTeamToUpdate.AddPlayer(player);
                        }
                    }
                    else
                    {
                        if (fantasyPlayers.Contains(player.PlayerID))
                        {
                            fantasyTeamToUpdate.Players.Remove(player);
                        }
                    }
                }
                catch(Exception e)
                { e.ToString(); }
              
            }
            db.SaveChanges();
        }

        //Get: ViewScore/round1
        public ActionResult ViewScore(int? TeamID)
        {
            if (TeamID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fantasyTeam = db.FantasyTeams.FirstOrDefault(t => t.TeamID == TeamID);
      
            var fixtures = db.Fixtures.ToList().Where(f => f.gamePlayed == true && f.RoundStage == RoundStage.FirstRound);
            //var fantasyTeam = db.FantasyTeams.Include(t=>t.Players).Where(t => t.TeamID == id).SingleOrDefault();
            //if (fantasyTeam == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            //}
            var players = fantasyTeam.Players;
            var viewModel = new List<FantasyTeamScoreUpdate>();

            foreach(var f in fixtures)
            {
                foreach(var p in players)
                {
                    var score = db.PlayerRoundStats.ToList().Where(s => s.FixtureId == f.FixtureId && s.PlayerID == p.PlayerID).SingleOrDefault();
                    viewModel.Add(new FantasyTeamScoreUpdate
                    {
                       // FantasyTeamName = fantasyTeam.TeamName,
                        PlayerName = p.PlayerFirstname + " " + p.PlayerSurname,
                        Position = p.Position,
                        Team = p.NationCode,
                        Score = score.TotalPoints
                    });
                }
            }
            ViewBag.TeamScore = viewModel;
            return View(fantasyTeam);
        }
      
        // GET: FantasyTeam/Delete/5
        public ActionResult Delete(int? TeamID)
        {
            if (TeamID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyTeam fantasyTeam = db.FantasyTeams.Find(TeamID);
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
