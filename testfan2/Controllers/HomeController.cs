using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using testfan2.ViewModels;
using System.Security.Claims;
using testfan2.Models;
using System.Data;
using System.Data.Entity;

namespace testfan2.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
          
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;


                    // var user = User.Identity.GetUserId();
                    if (userIdValue != null)
                    {
                        //var team = new FantasyTeam();
                       
                        var team = db.FantasyTeams.Include(t => t.Players).Where(t => t.UserId == userIdValue).FirstOrDefault();
                        if (team != null)
                        {
                       
                            team.Players = db.FantasyTeams.Where(t => t.TeamID == team.TeamID).Single().Players;
                            var teamName = team.TeamName;
                            var owner = team.User.CustomerFirstName + " " + team.User.CustomerSurname;

                            var gk = team.Players.Where(p => p.Position == Position.GoalKeeper).ToArray();
                            var def = team.Players.Where(p => p.Position == Position.Defender).ToArray();
                            var mid = team.Players.Where(p => p.Position == Position.Midfielder).ToArray();
                            var fwd = team.Players.Where(p => p.Position == Position.Forward).ToArray();
  
                            ViewBag.DEF = def;
                            ViewBag.GK = gk;
                            ViewBag.MID = mid;
                            ViewBag.FWD = fwd;
                            ViewBag.TeamName = teamName;
                            ViewBag.Owner = owner;

                            //display top 5 players
                            var top5Players = db.PlayerRoundStats.ToList().OrderByDescending(p => p.TotalPoints).Take(5);
                            ViewBag.top5players = top5Players.ToArray();

                            //display top 5 teams overall
                            var top5Teams = db.FantasyTeams.ToList().OrderByDescending(t => t.OverallScore).Take(5);
                            ViewBag.top5teams = top5Teams.ToArray();

                          
                            //display top 5 goalscorers
                            var players = from pl in db.PlayerRoundStats
                                          group pl by new { pl.PlayerID, pl.Player.PlayerSurname } into pls
                                          select new TopScorerView { playerId = pls.Key.PlayerID, playerName = pls.Key.PlayerSurname, Goals = pls.Sum(pl => pl.goalScored) };
                                        ;
                            var goalscorers = players.OrderByDescending(g => g.Goals).Take(5);
                            ViewBag.top5goalscorers = goalscorers.ToArray();
                            return View(team);
                        }
                    }
                }
            }
           
          
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "The best Euro 2016 Fantasy Football App";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Fantasy Football Euro 2016 contact page.";

            return View();
        }
    }
}