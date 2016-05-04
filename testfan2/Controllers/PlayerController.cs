using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testfan2.Models;
using PagedList;



namespace testfan2.Controllers
{
    public class PlayerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Player
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var players = db.Players.Include(r => r.NationTeam);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.TeamSortParm = String.IsNullOrEmpty(sortOrder) ? "Team_desc" : "";
           // ViewBag.PointsSortParm = sortOrder == "TotalPoints" ? "points_desc" : "TotalPoints";
            ViewBag.PositionSortParm = sortOrder == "Position" ? "position_desc" : "Position";
            ViewBag.ValueSortParm = sortOrder == "Value" ? "value_desc" : "Value";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                players = players.Where(r => r.PlayerSurname.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Team_desc":
                    players = players.OrderByDescending(r => r.NationTeam.NationCode);
                    break;
                //case "TotalPoints":
                //    players = players.OrderBy(r => r.TotalPoints);
                //    break;
                //case "points_desc":
                //    players = players.OrderByDescending(r => r.TotalPoints);
                //    break;
                case "Postion":
                    players = players.OrderBy(r => r.Position);
                    break;
                case "position_desc":
                    players = players.OrderByDescending(r => r.Position);
                    break;
                case "Value":
                    players = players.OrderBy(r => r.PlayerValue);
                    break;
                case "value_desc":
                   players = players.OrderByDescending(r => r.PlayerValue);
                    break;
                default:
                    players = players.OrderBy(r => r.NationTeam.NationCode);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
           
            return View(players.ToPagedList(pageNumber, pageSize));
        }

        // GET: Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Player/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
          //  var id = db.Players.Max(i => i.PlayerID) + 1;
            Player player = new Player() { DateOfBirth = DateTime.Now};
            ViewBag.NationCode = new SelectList(db.NationTeams, "NationCode", "NationCode");
            return View(player);
        }

        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerID,PlayerFirstname,PlayerSurname,Position,DateOfBirth,PlayerValue,NationCode,GoalWeight")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }
        [Authorize(Roles = "Administrator")]
        // GET: Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerID,PlayerFirstname,PlayerSurname,Position,DateOfBirth,PlayerValue,NationCode,GoalWeight")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }
        [Authorize(Roles = "Administrator")]
        // GET: Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
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
