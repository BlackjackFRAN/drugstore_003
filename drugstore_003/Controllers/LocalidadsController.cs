using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using drugstore_003.Models;

namespace drugstore_003.Controllers
{
    public class LocalidadsController : Controller
    {
        private Bd_drugstore_002Entities db = new Bd_drugstore_002Entities();

        // GET: Localidads
        public ActionResult Index()
        {
            var localidads = db.Localidads.Include(l => l.Provincias);
            return View(localidads.ToList());
        }

        // GET: Localidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidads localidads = db.Localidads.Find(id);
            if (localidads == null)
            {
                return HttpNotFound();
            }
            return View(localidads);
        }

        // GET: Localidads/Create
        public ActionResult Create()
        {
            ViewBag.idProvincia = new SelectList(db.Provincias, "idProvincia", "nombre");
            return View();
        }

        // POST: Localidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoPostal,nombre,idProvincia")] Localidads localidads)
        {
            if (ModelState.IsValid)
            {
                db.Localidads.Add(localidads);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProvincia = new SelectList(db.Provincias, "idProvincia", "nombre", localidads.idProvincia);
            return View(localidads);
        }

        // GET: Localidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidads localidads = db.Localidads.Find(id);
            if (localidads == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProvincia = new SelectList(db.Provincias, "idProvincia", "nombre", localidads.idProvincia);
            return View(localidads);
        }

        // POST: Localidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoPostal,nombre,idProvincia")] Localidads localidads)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localidads).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProvincia = new SelectList(db.Provincias, "idProvincia", "nombre", localidads.idProvincia);
            return View(localidads);
        }

        // GET: Localidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidads localidads = db.Localidads.Find(id);
            if (localidads == null)
            {
                return HttpNotFound();
            }
            return View(localidads);
        }

        // POST: Localidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Localidads localidads = db.Localidads.Find(id);
            db.Localidads.Remove(localidads);
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
