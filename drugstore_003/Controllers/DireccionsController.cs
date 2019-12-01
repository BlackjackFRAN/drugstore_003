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
    public class DireccionsController : Controller
    {
        private Bd_drugstore_002Entities db = new Bd_drugstore_002Entities();

        // GET: Direccions
        public ActionResult Index()
        {
            var direccions = db.Direccions.Include(d => d.Localidads);
            return View(direccions.ToList());
        }

        // GET: Direccions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccions direccions = db.Direccions.Find(id);
            if (direccions == null)
            {
                return HttpNotFound();
            }
            return View(direccions);
        }

        // GET: Direccions/Create
        public ActionResult Create()
        {
            ViewBag.codigoPostal = new SelectList(db.Localidads, "codigoPostal", "nombre");
            return View();
        }

        // POST: Direccions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDireccion,calle,numero,codigoPostal")] Direccions direccions)
        {
            if (ModelState.IsValid)
            {
                db.Direccions.Add(direccions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigoPostal = new SelectList(db.Localidads, "codigoPostal", "nombre", direccions.codigoPostal);
            return View(direccions);
        }

        // GET: Direccions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccions direccions = db.Direccions.Find(id);
            if (direccions == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigoPostal = new SelectList(db.Localidads, "codigoPostal", "nombre", direccions.codigoPostal);
            return View(direccions);
        }

        // POST: Direccions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDireccion,calle,numero,codigoPostal")] Direccions direccions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direccions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigoPostal = new SelectList(db.Localidads, "codigoPostal", "nombre", direccions.codigoPostal);
            return View(direccions);
        }

        // GET: Direccions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direccions direccions = db.Direccions.Find(id);
            if (direccions == null)
            {
                return HttpNotFound();
            }
            return View(direccions);
        }

        // POST: Direccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Direccions direccions = db.Direccions.Find(id);
            db.Direccions.Remove(direccions);
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
