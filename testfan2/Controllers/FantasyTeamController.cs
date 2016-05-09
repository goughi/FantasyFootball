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


namespace testfan2.Controllers
{
    public class FantasyTeamController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FantasyTeam of logged in user
        public ActionResult Index(string userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var team = db.FantasyTeams.FirstOrDefault(t => t.UserId == userId);
          
         //if there is a team order the players by goalkeeper, defender, midfielder then forwards
            if (team != null)
            {
               
                team.Players = db.FantasyTeams.Where(t => t.TeamID == team.TeamID).Single().Players;
                team.Players.OrderBy(c => c.Position == Position.GoalKeeper).ThenBy(c => c.Position == Position.Defender).ThenBy(c => c.Position == Position.Midfielder).ThenBy(c => c.Position == Position.Forward);
                return View(team);

            }
            return View();

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
        public ActionResult Create(string CustomerID)
        {
            if (CustomerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fantasyTeam = new FantasyTeam();
            fantasyTeam.Players = new List<Player>();
            fantasyTeam.IsConfirmed = false;

            fantasyTeam.UserId = db.Users.Where(c => c.Id == CustomerID).Single().Id;
            ViewBag.FantasyLeagueID = new SelectList(db.FantasyLeagues, "FantasyLeagueId", "FantasyLeagueName");
            return View(fantasyTeam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamID,TeamName,UserId,FantasyLeagueID")] FantasyTeam fantasyTeam)
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
        // POST: FantasyTeam/AddTeam/ TeamID
        public ActionResult AddTeam(int? TeamID)
        {
            if (TeamID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fantasyTeam = db.FantasyTeams.Include(p => p.Players).Where(t => t.TeamID == TeamID).Single();
            var teamName = fantasyTeam.TeamName;
            var owner = fantasyTeam.User.CustomerFirstName + " " + fantasyTeam.User.CustomerSurname;
            ViewBag.TeamName = teamName;
            ViewBag.Owner = owner; 
            //populate view with all players in database
            PopulateTeam(fantasyTeam);
            if (fantasyTeam == null)
            {
                return HttpNotFound();
            }
            return View(fantasyTeam);
        }
        
        // POST: FantasyTeam/AddTeam/ TeamID, string[] def, string[] mid, string[]fwd, string[]gk  
        // team should consist of a string array of one goalkeeper, an array of four defenders, an array of four midfielders and an array of two forwards
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
                catch(ArgumentException e)
                {
                    e.Message.ToString();
                }
            }
            //(TeamID);
            return RedirectToAction("Index","Home"); 
        }
        
        // GET: FantasyTeam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FantasyTeam fantasyTeam = db.FantasyTeams.Include(i => i.Players).Where(i => i.TeamID == id).Single();
            PopulateTeam(fantasyTeam);

            if (fantasyTeam == null)
            {
                return HttpNotFound();
            }
           
            return View(fantasyTeam);
        }
        //all players in database are shown in view under the following categories - goalkeeper, defender, midfielder and forward
        private void PopulateTeam(FantasyTeam fantasyTeam)
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
        // Edit is used to make transfers for team
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] def, string[] mid, string[] fwd, string[] gk)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var teamToAddPlayers = db.FantasyTeams
               .Include(i => i.Players)
               .Where(i => i.TeamID == id)
               .Single();

            if (TryUpdateModel(teamToAddPlayers, "",
    new string[] { "TeamName" }))
            {
                try
                {


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
                        if (defHS.Contains(player.PlayerID.ToString()) || midHS.Contains(player.PlayerID.ToString()) || fwdHS.Contains(player.PlayerID.ToString()) || gkHS.Contains(player.PlayerID.ToString()))
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
                catch (ArgumentException e)
                {
                    e.Message.ToString();

                }
            }
            
            return RedirectToAction("Index", "Home");
        }
        //UpdateFantasyPlayers checks to see if the checked player has already been added. If not this player is then added to team
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

        //Get: ViewScore/round1 - viewscore shows the players score for round one of this person's fantasy team
        public ActionResult ViewScore(int? TeamID)
        {
            if (TeamID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
      
            var fixtures = db.Fixtures.ToList().Where(f => f.gamePlayed == true && f.RoundStage == RoundStage.FirstRound);
            var fantasyTeam = db.FantasyTeams.Include(t=>t.Players).Where(t => t.TeamID == TeamID).SingleOrDefault();
            if (fantasyTeam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
            }
            var players = fantasyTeam.Players;
            var viewModel = new List<FantasyTeamScoreUpdate>();

            var totalScore = 0;
            foreach(var f in fixtures)
            {
                foreach(var p in players)
                {
                    var score = db.PlayerRoundStats.ToList().Where(s => s.FixtureId == f.FixtureId && s.PlayerID == p.PlayerID).SingleOrDefault();
                    if (score != null)
                    {
                        viewModel.Add(new FantasyTeamScoreUpdate
                        {
                           
                            PlayerName = p.PlayerFirstname + " " + p.PlayerSurname,
                            Position = p.Position,
                            Team = p.NationCode,
                            Score = score.TotalPoints
                        });
                        totalScore += score.TotalPoints;
                    }
                }
            }
            fantasyTeam.FirstRoundScore = totalScore;
            db.SaveChanges();
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
