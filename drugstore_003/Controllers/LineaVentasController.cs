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
    public class LineaVentasController : Controller
    {
        private Bd_drugstore_002Entities db = new Bd_drugstore_002Entities();

        // GET: LineaVentas
        public ActionResult Index()
        {
            var lineaVentas = db.LineaVentas.Include(l => l.Productoes).Include(l => l.Ventas);
            return View(lineaVentas.ToList());
        }

        // GET: LineaVentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaVentas lineaVentas = db.LineaVentas.Find(id);
            if (lineaVentas == null)
            {
                return HttpNotFound();
            }
            return View(lineaVentas);
        }

        // GET: LineaVentas/Create
        public ActionResult Create()
        {
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion");
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "total");
            return View();
        }

        // POST: LineaVentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLineaVenta,idVenta,idProducto,cantidad,subtotal,precio")] LineaVentas lineaVentas)
        {
            if (ModelState.IsValid)
            {
                db.LineaVentas.Add(lineaVentas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaVentas.idProducto);
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "total", lineaVentas.idVenta);
            return View(lineaVentas);
        }

        // GET: LineaVentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaVentas lineaVentas = db.LineaVentas.Find(id);
            if (lineaVentas == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaVentas.idProducto);
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "total", lineaVentas.idVenta);
            return View(lineaVentas);
        }

        // POST: LineaVentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLineaVenta,idVenta,idProducto,cantidad,subtotal,precio")] LineaVentas lineaVentas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lineaVentas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaVentas.idProducto);
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "total", lineaVentas.idVenta);
            return View(lineaVentas);
        }

        // GET: LineaVentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaVentas lineaVentas = db.LineaVentas.Find(id);
            if (lineaVentas == null)
            {
                return HttpNotFound();
            }
            return View(lineaVentas);
        }

        // POST: LineaVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LineaVentas lineaVentas = db.LineaVentas.Find(id);
            db.LineaVentas.Remove(lineaVentas);
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
