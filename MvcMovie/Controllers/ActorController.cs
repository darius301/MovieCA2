using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Controllers
{
    public class ActorsController : Controller
    {
        private MvcMovieDb db = new MvcMovieDb();

        #region Actor Index
        public ActionResult Index()
        {
            return View(db.Actors.ToList());
        }


        public ActionResult Details(int id = 0)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }
        #endregion

        #region Add Actor

        public ActionResult Create(int movID)
        {
            Actor ma = new Actor { MovieId = movID };

            return View(ma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Actor actor, int movID)
        {
            actor.MovieId = movID;
            if (ModelState.IsValid)
            {
                db.Actors.Add(actor);
                db.SaveChanges();
                return RedirectToAction("Details", "Home", new { id = actor.MovieId });
            }

            return View(actor);
        }
        #endregion


        #region Edit Actor
        public ActionResult Edit(int id)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Actor actor, int id)
        {
            if (ModelState.IsValid)
            {
                actor.ActorId = id;
                db.Entry(actor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Home", new { id = actor.MovieId });
            }
            return View(actor);
        }

        #endregion

        #region Delete Actor
        public ActionResult Delete(int id = 0)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actor actor = db.Actors.Find(id);
            db.Actors.Remove(actor);
            db.SaveChanges();
            return RedirectToAction("Details", "Home", new { id = actor.MovieId });
        }

        #endregion
    }
}
