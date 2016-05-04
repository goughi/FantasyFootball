using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
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
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
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
                            //var players = new List<Player>();
                            team.Players = db.FantasyTeams.Where(t => t.TeamID == team.TeamID).Single().Players;
                            //var p = team.Players.OrderBy(c => c.Position == Position.GoalKeeper).ThenBy(c => c.Position == Position.Defender).ThenBy(c => c.Position == Position.Midfielder).ThenBy(c => c.Position == Position.Forward).ToArray();
                            var gk = team.Players.Where(p => p.Position == Position.GoalKeeper).ToArray();
                            var def = team.Players.Where(p => p.Position == Position.Defender).ToArray();
                            var mid = team.Players.Where(p => p.Position == Position.Midfielder).ToArray();
                            var fwd = team.Players.Where(p => p.Position == Position.Forward).ToArray();
                            //team.Players = team.Players.ToArray();
                            ViewBag.DEF = def;
                            ViewBag.GK = gk;
                            ViewBag.MID = mid;
                            ViewBag.FWD = fwd;
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