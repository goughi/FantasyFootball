using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Mvc;
using testfan2.Models;
using testfan2.ViewModels;
using System.Security.Claims;

namespace testfan2.Controllers
{
    public class FixtureController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fixture
        public ActionResult Index()
        {
            //add a viewbag of teamid in order to pass teamid parameter to make a transfer
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

                        var team = db.FantasyTeams.Include(t => t.Players).Where(t => t.UserId == userIdValue).FirstOrDefault();
                        if (team != null)
                        {
                            var teamId = team.TeamID;
                            ViewBag.TeamId = teamId;
                            var round = RoundStage.FirstRound;
                            ViewBag.RoundStage = round;
                        }
                    }
                }
            }
            var fixtures = db.Fixtures.Include(f => f.AwayTeam).Include(f => f.HomeTeam).Where(f=>f.RoundStage == RoundStage.FirstRound);
            return View(fixtures.ToList());
        }

        // GET: Fixture next round
        public ActionResult NextRound(RoundStage round)
        {
            //add a viewbag of teamid in order to pass teamid parameter to make a transfer
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

                        var team = db.FantasyTeams.Include(t => t.Players).Where(t => t.UserId == userIdValue).FirstOrDefault();
                        if (team != null)
                        {
                            var teamId = team.TeamID;
                            ViewBag.TeamId = teamId;
                            ViewBag.RoundStage = round;
                        }
                    }
                }
            }
            var fixtures = db.Fixtures.Include(f => f.AwayTeam).Include(f => f.HomeTeam).Where(f => f.RoundStage == round);
            return View(fixtures.ToList());
        }
        // GET: Fixture/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fixture = db.Fixtures.Include(i => i.AwayTeamScorer).Where(i => i.FixtureId == id).Single();
            if (fixture == null)
            {
                return HttpNotFound();
            }
            return View(fixture);
        }


        // GET: Fixture/PlayGame/2
        public ActionResult PlayGame(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fixture fixture = db.Fixtures.Find(id);
            var home = db.NationTeams.Where(n => n.NationCode == fixture.HomeTeamNationCode).Single();
            var away = db.NationTeams.Where(n => n.NationCode == fixture.AwayTeamNationCode).Single();

            int homeScore = GetHomeTeamScore(home, away);
            int awayScore = GetAwayTeamScore(home, away);
            fixture.HomeTeamScore = homeScore;
            fixture.AwayTeamScore = awayScore;

            
            populateGameDetails(homeScore, awayScore, fixture, home, away);
          
            if (fixture == null)
            {
                return HttpNotFound();
            }
            ViewBag.AwayTeamNationCode = new SelectList(db.NationTeams, "NationCode", "NationCode", fixture.AwayTeamNationCode);
            ViewBag.HomeTeamNationCode = new SelectList(db.NationTeams, "NationCode", "NationCode", fixture.HomeTeamNationCode);
            return View(fixture);

        }

        public bool populateGameDetails(int homeScore, int awayScore, Fixture fixture, NationTeam home, NationTeam away)
        {
            var homePlayers = db.Players.ToList().Where(n => n.NationCode == fixture.HomeTeamNationCode);
            var awayPlayers = db.Players.ToList().Where(n => n.NationCode == fixture.AwayTeamNationCode);
            var viewHomeScorers = new List<ScorerIndexData>();
            var viewHomeYellows = new List<ScorerIndexData>();
            var viewHomeReds = new List<ScorerIndexData>();
            var viewAwayScorers = new List<ScorerIndexData>();
            var viewAwayYellows = new List<ScorerIndexData>();
            var viewAwayReds = new List<ScorerIndexData>();
            var viewMOTM = new ScorerIndexData();

            Random yellowRandom = new Random();
            foreach (var player in homePlayers)
            {
                var yellow = yellowRandom.NextDouble();
                //this means yellow card
                if (yellow < .15)
                {
                    viewHomeYellows.Add(new ScorerIndexData
                    {
                        playerId = player.PlayerID,
                        playerName = player.PlayerFirstname.Take(1).Single() + ". " + player.PlayerSurname,
                    });
                }
                //this means red card
                if (yellow > .96)
                {
                    viewHomeReds.Add(new ScorerIndexData
                    {
                        playerId = player.PlayerID,
                        playerName = player.PlayerFirstname.Take(1).Single() + ". " + player.PlayerSurname,
                    });
                }
            }
            foreach (var player in awayPlayers)
            {
                var yellow = yellowRandom.NextDouble();
                //this means yellow card
                if (yellow < .15)
                {
                    viewAwayYellows.Add(new ScorerIndexData
                    {
                        playerId = player.PlayerID,
                        playerName = player.PlayerFirstname.Take(1).Single() + ". " + player.PlayerSurname,
                    });
                }
                //this means red card
                if (yellow > .96)
                {
                    viewAwayReds.Add(new ScorerIndexData
                    {
                        playerId = player.PlayerID,
                        playerName = player.PlayerFirstname.Take(1).Single() + ". " + player.PlayerSurname,
                    });
                }
            }
            if (homeScore > 0)
            {
               
                Random random = new Random();
                for (int i = 0; i < homeScore;i++)
                {
                    var rnd = random.NextDouble();
                    foreach (var player in homePlayers)
                    {
                       
                        if (rnd < player.GoalWeight)
                        {
                            viewHomeScorers.Add(new ScorerIndexData
                            {
                                playerId = player.PlayerID,
                                playerName = player.PlayerFirstname.Take(1).Single() + ". " + player.PlayerSurname,
                                //scored = true
                            });
                            // fixture.HomeTeamScorer.Add(player);
                            //db.SaveChanges();
                            break;
                            //rnd = random.NextDouble();
                        }
                        rnd -= player.GoalWeight;
                    }
                }
            }
            //find scorer and display on screen
            if (awayScore > 0)
            {
              
                Random random = new Random();
                for (int i = 0; i < awayScore; i++)
                {
                    var rnd = random.NextDouble();
                    foreach (var player in awayPlayers)
                    {
                        
                        if (rnd < player.GoalWeight)
                        {
                            viewAwayScorers.Add(new ScorerIndexData
                            {
                                playerId = player.PlayerID,
                                playerName = player.PlayerFirstname.Take(1).Single() + ". " + player.PlayerSurname,
                                scored = true
                            });
                            // fixture.HomeTeamScorer.Add(player);
                            //db.SaveChanges();
                            break;
                            //rnd = random.NextDouble();
                        }
                        rnd -= player.GoalWeight;
                    }
                }
            }

            //update cleansheet and goal conceded stat - applicable only for defenders and goalkeepers
            var awayGkdef = awayPlayers.Where(p => p.Position == Position.GoalKeeper || p.Position == Position.Defender);
            if(homeScore == 0)
            {
                foreach(var player in awayGkdef)
                {
                    var stat = db.PlayerRoundStats.Where(s => s.PlayerID == player.PlayerID && s.FixtureId == fixture.FixtureId).FirstOrDefault();
                    stat.CleanSheet = true;
                    db.SaveChanges();
                }
            }
            else
            {
                foreach (var player in awayGkdef)
                {
                    var stat = db.PlayerRoundStats.Where(s => s.PlayerID == player.PlayerID && s.FixtureId == fixture.FixtureId).FirstOrDefault();
                    stat.GoalsConceded = homeScore;
                    db.SaveChanges();
                }
            }
            var homeGkdef = homePlayers.Where(p => p.Position == Position.GoalKeeper || p.Position == Position.Defender);
            if (awayScore == 0)
            {
                foreach (var player in homeGkdef)
                {
                    var stat = db.PlayerRoundStats.Where(s => s.PlayerID == player.PlayerID && s.FixtureId == fixture.FixtureId).FirstOrDefault();
                    stat.CleanSheet = true;
                    db.SaveChanges();
                }
            }
            else
            {
                foreach (var player in homeGkdef)
                {
                    var stat = db.PlayerRoundStats.Where(s => s.PlayerID == player.PlayerID && s.FixtureId == fixture.FixtureId).FirstOrDefault();
                    stat.GoalsConceded = awayScore;
                    db.SaveChanges();
                }
            }
            //if players team wins add to stats
            if (homeScore > awayScore)
            {
                foreach(var p in home.Players)
                {
                    var statistic = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId && st.PlayerID == p.PlayerID).FirstOrDefault();
                    statistic.IsWin = true;
                    db.SaveChanges();
                }

                //find man of the match with a min and max value of random number (0,10) - indedexer for 11 players of winning team
                Random randomMOTM = new Random();
                int thisMOTM = randomMOTM.Next(0, 10);
                viewMOTM.playerId = home.Players.ToArray()[thisMOTM].PlayerID;
                viewMOTM.playerName = home.Players.ToArray()[thisMOTM].PlayerFirstname + ". " + home.Players.ToArray()[thisMOTM].PlayerSurname + " 0f " + home.Nation + " played slendid and was man of the match!" ;
                //add to stats
                var stat = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId && st.PlayerID == viewMOTM.playerId).FirstOrDefault();
                stat.ManOfTheMatch = true;
                db.SaveChanges();
               
            }

            if(homeScore == awayScore)
            {
                //find man of the match with a min and max value of random number (0,21) - indedexer 0 T0 10 for hometeam, 11 to 21 for awayteam
                Random randomMOTM = new Random();
                int thisMOTM = randomMOTM.Next(0, 21);
                if (thisMOTM > 10)
                {
                    thisMOTM -= 11;
                    viewMOTM.playerId = away.Players.ToArray()[thisMOTM].PlayerID;
                    viewMOTM.playerName = away.Players.ToArray()[thisMOTM].PlayerFirstname + ". " + away.Players.ToArray()[thisMOTM].PlayerSurname + " 0f " + away.Nation + " played slendid and was man of the match!";
                }
                else
                {
                    viewMOTM.playerId = home.Players.ToArray()[thisMOTM].PlayerID;
                    viewMOTM.playerName = home.Players.ToArray()[thisMOTM].PlayerFirstname + ". " + home.Players.ToArray()[thisMOTM].PlayerSurname + " 0f " + home.Nation + " played slendid and was man of the match!";
                }
                //add to stats
                var stat = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId && st.PlayerID == viewMOTM.playerId).FirstOrDefault();
                stat.ManOfTheMatch = true;
                db.SaveChanges();
            }

            //if players team wins add to stats
            if (awayScore > homeScore)
            {
                foreach (var p in away.Players)
                {
                    var statistic = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId && st.PlayerID == p.PlayerID).FirstOrDefault();
                    statistic.IsWin = true;
                    db.SaveChanges();
                }
                Random randomMOTM = new Random();
                int thisMOTM = randomMOTM.Next(0, 10);
                viewMOTM.playerId = away.Players.ToArray()[thisMOTM].PlayerID;
                viewMOTM.playerName = away.Players.ToArray()[thisMOTM].PlayerFirstname + ". " + away.Players.ToArray()[thisMOTM].PlayerSurname + " 0f " + home.Nation + " played slendid and was man of the match!";
                //add to stats
                var stat = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId && st.PlayerID == viewMOTM.playerId).FirstOrDefault();
                stat.ManOfTheMatch = true;
                db.SaveChanges();

            }

            //FIND MINUTES PLAYED   -  first find subs   - the rest of team play 90 minutes
            //make 3 subs for both teams. Use a random number to select the sub and another random number to select minutes played
            //if team wins make late subs (between 55 and 89 minutes)
            //if team loses make earlier subs (between 30 and 80 minutes)
            //if draw (between 45 and 80 minutes)

            Random time = new Random();
            int[] homesubs = CallSubs();
            int[] awaysubs = CallSubs();
            if (homeScore > awayScore)
            {
              
                foreach (var p in homesubs)
                {
                    var statistic = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId && st.PlayerID == home.Players.ToArray()[p].PlayerID).FirstOrDefault();
                    statistic.MinutesPlayed = time.Next(55,89);
                    db.SaveChanges();
                }
                foreach (var p in awaysubs)
                {
                    var statistic = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId && st.PlayerID == away.Players.ToArray()[p].PlayerID).FirstOrDefault();
                    statistic.MinutesPlayed = time.Next(30, 80);
                    db.SaveChanges();
                }

            }
            else if(awayScore > homeScore)
            {

                foreach (var p in homesubs)
                {
                    var statistic = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId && st.PlayerID == home.Players.ToArray()[p].PlayerID).FirstOrDefault();
                    statistic.MinutesPlayed = time.Next(30, 80);
                    db.SaveChanges();
                }
                foreach (var p in awaysubs)
                {
                    var statistic = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId && st.PlayerID == away.Players.ToArray()[p].PlayerID).FirstOrDefault();
                    statistic.MinutesPlayed = time.Next(55, 89);
                    db.SaveChanges();
                }
            }
            else
            {
                foreach (var p in homesubs)
                {
                    var statistic = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId && st.PlayerID == home.Players.ToArray()[p].PlayerID).FirstOrDefault();
                    statistic.MinutesPlayed = time.Next(45, 80);
                    db.SaveChanges();
                }
                foreach (var p in awaysubs)
                {
                    var statistic = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId && st.PlayerID == away.Players.ToArray()[p].PlayerID).FirstOrDefault();
                    statistic.MinutesPlayed = time.Next(45, 80);
                    db.SaveChanges();
                }
            }
            //FIND MIN PLATED = 90 MINUTES
            var fullGamePlayers = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixture.FixtureId);
            foreach(var stat in fullGamePlayers)
            {
                if(stat.MinutesPlayed == 0)
                {
                    stat.MinutesPlayed = 90;
                    db.SaveChanges();
                }
            }

            ViewBag.AwayScorers = viewAwayScorers;
            ViewBag.HomeScorers = viewHomeScorers;
            ViewBag.HomeYellows = viewHomeYellows;
            ViewBag.HomeReds = viewHomeReds;
            ViewBag.AwayYellows = viewAwayYellows;
            ViewBag.AwayReds = viewAwayReds;
            ViewBag.MOTM = viewMOTM;
            return true;
        }
        private int[] CallSubs()
        {
            const int sub = 3;
            Random s = new Random();
            List<int> subs = new List<int>();
            for (int i = 0; i < sub; i++)
            {
                int nextSub = s.Next(0, 10);
                if (!subs.Contains(nextSub))
                    subs.Add(nextSub);
            }
            return subs.ToArray();
        }
        // POST: Fixture/PlayGame/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlayGame(int? id, FormCollection collection, string[]awayscorer, string[]homescorer, string[]homeredcards, string[]homeyellowcards, string[]awayredcards, string[]awayyellowcards)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var fixtureResult = db.Fixtures.Include(i => i.HomeTeamScorer).Include(i=>i.AwayTeamScorer).Where(i => i.FixtureId == id).Single();
            if (TryUpdateModel(fixtureResult))
            {
                try
                {
                    if (fixtureResult.AwayTeamScorer == null)
                    {
                        fixtureResult.AwayTeamScorer = new List<Player>();

                    }
                    if (awayscorer == null)
                    {
                        fixtureResult.AwayTeamScorer = new List<Player>();

                    }

                    //var awayScorersHS = new HashSet<string>(awayscorer);
                    //var fixtureAwayScorers = new HashSet<int>(fixtureResult.AwayTeamScorer.Select(p => p.PlayerID));
                    //foreach (var player in db.Players)
                    //{
                    //    if (awayScorersHS.Contains(player.PlayerID.ToString()))
                    //    {
                    //        if (!fixtureAwayScorers.Contains(player.PlayerID))
                    //        {
                    //            fixtureResult.AwayTeamScorer.Add(player);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (fixtureAwayScorers.Contains(player.PlayerID))
                    //        {
                    //            fixtureResult.AwayTeamScorer.Remove(player);
                    //        }
                    //    }
                    //}
                    // var updatedFixture = db.Fixtures.Include(i => i.HomeTeamScorer).Include(i => i.AwayTeamScorer).Where(i => i.FixtureId == id).Single();
                    fixtureResult.gamePlayed = true;
                    db.SaveChanges();
                    playerStat(fixtureResult, awayscorer, homescorer, homeredcards, homeyellowcards, awayredcards, awayyellowcards);
                    if (fixtureResult.RoundStage == RoundStage.FirstRound)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("NextRound", new { round = RoundStage.SecondRound });
                    }
                

                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
           
            
       
            ViewBag.AwayTeamNationCode = new SelectList(db.NationTeams, "NationCode", "NationCode", fixtureResult.AwayTeamNationCode);
            ViewBag.HomeTeamNationCode = new SelectList(db.NationTeams, "NationCode", "NationCode", fixtureResult.HomeTeamNationCode);
            return View(fixtureResult);
        }

        private void playerStat(Fixture fixtureResult, string[] awayscorer, string[] homescorer, string[] homeredcards, string[] homeyellowcards, string[]awayredcards, string[]awayyellowcards)
        {
            //if (awayscorer == null)
            //{
            //    fixtureResult.AwayTeamScorer = new List<Player>();
            //    return;
            //}
            using (var db = new ApplicationDbContext()) {
              
 
                //var fixtureAwayScorers = new HashSet<int>(fixtureResult.AwayTeamScorer.Select(p => p.PlayerID));
                //var fixtureHomeScorers = new HashSet<int>(fixtureResult.HomeTeamScorer.Select(p => p.PlayerID));
                var players = db.Players.ToList();
                foreach (var player in players)
                {
                    if (awayscorer != null)
                    {
                        var awayScorersHS = new HashSet<string>(awayscorer);
                        if (awayScorersHS.Contains(player.PlayerID.ToString()))
                        {

                            var stat = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixtureResult.FixtureId && st.PlayerID == player.PlayerID).FirstOrDefault();
                            var away = db.Players.ToList().Where(p => p.PlayerID == player.PlayerID).Single();
                            stat.goalScored += 1;
                            fixtureResult.AwayTeamScorer.Add(away);
                            db.SaveChanges();

                        }
                    }
                    if (homescorer != null)
                    {
                        var homeScorersHS = new HashSet<string>(homescorer);
                        if (homeScorersHS.Contains(player.PlayerID.ToString()))
                        {

                            var stat = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixtureResult.FixtureId && st.PlayerID == player.PlayerID).FirstOrDefault();
                            stat.goalScored += 1;
                            db.SaveChanges();

                        }
                    }
                    if (homeredcards != null)
                    {
                        var homeRedCardsHS = new HashSet<string>(homeredcards);
                        if (homeRedCardsHS.Contains(player.PlayerID.ToString()))
                        {

                            var stat = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixtureResult.FixtureId && st.PlayerID == player.PlayerID).FirstOrDefault();
                            stat.RedCarded = true;
                            db.SaveChanges();

                        }
                    }
                    if (homeyellowcards != null)
                    {
                        var homeYellowCardsHS = new HashSet<string>(homeyellowcards);
                        if (homeYellowCardsHS.Contains(player.PlayerID.ToString()))
                        {

                            var stat = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixtureResult.FixtureId && st.PlayerID == player.PlayerID).FirstOrDefault();
                            stat.YellowCarded = true;
                            db.SaveChanges();

                        }
                    }
                    if (awayredcards != null)
                    {
                        var awayRedCardsHS = new HashSet<string>(awayredcards);
                        if (awayRedCardsHS.Contains(player.PlayerID.ToString()))
                        {

                            var stat = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixtureResult.FixtureId && st.PlayerID == player.PlayerID).FirstOrDefault();
                            stat.RedCarded = true;
                            db.SaveChanges();

                        }
                    }
                    if (awayyellowcards != null)
                    {
                        var awayYellowCardsHS = new HashSet<string>(awayyellowcards);
                        if (awayYellowCardsHS.Contains(player.PlayerID.ToString()))
                        {

                            var stat = db.PlayerRoundStats.ToList().Where(st => st.FixtureId == fixtureResult.FixtureId && st.PlayerID == player.PlayerID).FirstOrDefault();
                            stat.YellowCarded = true;
                            db.SaveChanges();

                        }
                    }

                }
                db.SaveChanges();
            }
        }

        // GET: Fixture/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fixture fixture = db.Fixtures.Find(id);
            if (fixture == null)
            {
                return HttpNotFound();
            }
            return View(fixture);
        }


        public int GetHomeTeamScore(NationTeam homeTeam, NationTeam awayTeam)
        {

            double[] weight = new double[] { };

            int[] list = Goal;
            if (homeTeam.TeamGoalWeight == awayTeam.TeamGoalWeight)
            {
                weight = SameGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight + 1) == awayTeam.TeamGoalWeight)
            {
                weight = BetterByOneGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight + 2) == awayTeam.TeamGoalWeight)
            {
                weight = BetterByTwoGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight + 3) == awayTeam.TeamGoalWeight)
            {
                weight = BetterByThreeGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight - 1) == awayTeam.TeamGoalWeight)
            {
                weight = WorseByOneGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight - 2) == awayTeam.TeamGoalWeight)
            {
                weight = WorseByTwoGoalWeight;
            }
            else if ((homeTeam.TeamGoalWeight - 3) == awayTeam.TeamGoalWeight)
            {
                weight = WorseByThreeGoalWeight;
            }
            var weighed_list = generateWeighedList(list, weight);
            Random random = new Random();
            int Myrandom = getRandomNumber(0, weighed_list.Count - 1);

            int homeTeamGoal = (weighed_list[Myrandom]);
            return homeTeamGoal;

        }
        public int GetAwayTeamScore(NationTeam homeTeam, NationTeam awayTeam)
        {

            double[] weight = new double[] { };

            int[] list = Goal;
            if (awayTeam.TeamGoalWeight == homeTeam.TeamGoalWeight)
            {
                weight = SameGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight + 1) == homeTeam.TeamGoalWeight)
            {
                weight = BetterByOneGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight + 2) == homeTeam.TeamGoalWeight)
            {
                weight = BetterByTwoGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight + 3) == homeTeam.TeamGoalWeight)
            {
                weight = BetterByThreeGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight - 1) == homeTeam.TeamGoalWeight)
            {
                weight = WorseByOneGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight - 2) == homeTeam.TeamGoalWeight)
            {
                weight = WorseByTwoGoalWeight;
            }
            else if ((awayTeam.TeamGoalWeight - 3) == homeTeam.TeamGoalWeight)
            {
                weight = WorseByThreeGoalWeight;
            }
            var weighed_list = generateWeighedList(list, weight);
            Random random = new Random();
            int Myrandom = getRandomNumber(0, weighed_list.Count - 1);

            int awayTeamGoal = (weighed_list[Myrandom]);
            return awayTeamGoal;

        }

        public string[] HomeTeamScorer { get; set; }
        public string[] AwayTeamScorer { get; set; }
        public string[] YellowCards { get; set; }
        public string[] RedCards { get; set; }
        //goal probability 
        public static readonly int[] Goal = { 0, 1, 2, 3, 4, 5 };
        public static readonly double[] SameGoalWeight = { .2, .3, .25, .15, .07, .03 };
        public static readonly double[] BetterByOneGoalWeight = { .1, .29, .3, .18, .09, .04 };
        public static readonly double[] BetterByTwoGoalWeight = { .08, .25, .3, .2, .11, .06 };
        public static readonly double[] BetterByThreeGoalWeight = { .06, .2, .29, .23, .13, .08 };
        public static readonly double[] WorseByOneGoalWeight = { .4, .34, .2, .04, .02 };
        public static readonly double[] WorseByTwoGoalWeight = { .43, .36, .18, .021, .009 };
        public static readonly double[] WorseByThreeGoalWeight = { .5, .36, .13, .006, .004 };

        public static int getRandomNumber(int min, int max)
        {
            Random random = new Random();
            int randomNumber = random.Next(min, max);
            return randomNumber;
        }

        public static List<int> generateWeighedList(int[] list, double[] weight)
        {
            List<int> weighed_list = new List<int>();
            // Loop over weights
            for (var i = 0; i < weight.Length; i++)
            {
                var multiples = weight[i] * 100;

                // Loop over the list of items
                for (var j = 0; j < multiples; j++)
                {
                    weighed_list.Add(list[i]);
                }
            }

            return weighed_list;

        }


        // POST: Fixture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fixture fixture = db.Fixtures.Find(id);
            db.Fixtures.Remove(fixture);
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
