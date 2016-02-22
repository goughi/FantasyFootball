using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testfan2.Models;
using testfan2.ViewModels;

namespace testfan2.Controllers
{
    public class RoundTeamController : Controller
    {
        FantasyFootballContext db = new FantasyFootballContext();
        // GET: RoundTeam
        public ActionResult Index(int? roundTeamId)
        {
            if (roundTeamId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoundTeam roundTeam = db.RoundTeams.Find(roundTeamId);
            if (roundTeam == null)
            {
                return HttpNotFound();
            }
           
            var viewModel = new RoundTeamViewModel
            {
                RoundTeamPlayers = roundTeam.GetRoundTeam(),
                teamTotal = roundTeam.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }

        // GET: /RoundTeam/AddPlayer/5
        public ActionResult AddPlayer(int id)
        {
            // Retrieve the player from the database
            var addedPlayer = db.Players
                .Single(player => player.PlayerID == id);

            // Add it to the team
            var roundTeam = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedAlbum);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
    }
}