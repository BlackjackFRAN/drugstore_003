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
    public class ProveedorsController : Controller
    {
        private Bd_drugstore_002Entities db = new Bd_drugstore_002Entities();

        // GET: Proveedors
        public ActionResult Index()
        {
            var proveedors = db.Proveedors.Include(p => p.Direccions);
            return View(proveedors.ToList());
        }

        // GET: Proveedors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedors proveedors = db.Proveedors.Find(id);
            if (proveedors == null)
            {
                return HttpNotFound();
            }
            return View(proveedors);
        }

        // GET: Proveedors/Create
        public ActionResult Create()
        {
            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle");
            return View();
        }

        // POST: Proveedors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProveedor,idDireccion,nombre,apellido,cuit")] Proveedors proveedors)
        {
            if (ModelState.IsValid)
            {
                db.Proveedors.Add(proveedors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", proveedors.idDireccion);
            return View(proveedors);
        }

        // GET: Proveedors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedors proveedors = db.Proveedors.Find(id);
            if (proveedors == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", proveedors.idDireccion);
            return View(proveedors);
        }

        // POST: Proveedors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProveedor,idDireccion,nombre,apellido,cuit")] Proveedors proveedors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", proveedors.idDireccion);
            return View(proveedors);
        }

        // GET: Proveedors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedors proveedors = db.Proveedors.Find(id);
            if (proveedors == null)
            {
                return HttpNotFound();
            }
            return View(proveedors);
        }

        // POST: Proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedors proveedors = db.Proveedors.Find(id);
            db.Proveedors.Remove(proveedors);
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
