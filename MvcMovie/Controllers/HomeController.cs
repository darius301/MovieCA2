using System.Linq;
using System.Web.Mvc;
using MvcMovie.Models;
using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web;



namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        private MvcMovieDb db = new MvcMovieDb();

        #region
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }
        #endregion

        #region Movie Details
        public ActionResult Details(int id = 0)
        {
            Movie m = db.Movies.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            else
            {
                m.Actors = (from e in db.Actors
                            where e.MovieId.Equals(id)
                            select e).ToList();
            }

            return View(m);
        }
        #endregion

        #region Create Movie
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }
        #endregion

        #region Edit Movie
        public ActionResult Edit(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }
        #endregion

        #region Delete Movie
        public ActionResult Delete(int id = 0)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                movie.Actors = (from e in db.Actors
                                where e.MovieId.Equals(id)
                                select e).ToList();
            }
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}


