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
    public class LineaComprasController : Controller
    {
        private Bd_drugstore_002Entities db = new Bd_drugstore_002Entities();

        // GET: LineaCompras
        public ActionResult Index()
        {
            var lineaCompras = db.LineaCompras.Include(l => l.Compras).Include(l => l.Productoes);
            return View(lineaCompras.ToList());
        }

        // GET: LineaCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaCompras lineaCompras = db.LineaCompras.Find(id);
            if (lineaCompras == null)
            {
                return HttpNotFound();
            }
            return View(lineaCompras);
        }

        // GET: LineaCompras/Create
        public ActionResult Create()
        {
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra");
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion");
            return View();
        }

        // POST: LineaCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLineaCompra,idCompra,idProducto,cantidad,subtotal,precioCompra")] LineaCompras lineaCompras)
        {
            if (ModelState.IsValid)
            {
                db.LineaCompras.Add(lineaCompras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra", lineaCompras.idCompra);
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaCompras.idProducto);
            return View(lineaCompras);
        }

        // GET: LineaCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaCompras lineaCompras = db.LineaCompras.Find(id);
            if (lineaCompras == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra", lineaCompras.idCompra);
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaCompras.idProducto);
            return View(lineaCompras);
        }

        // POST: LineaCompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLineaCompra,idCompra,idProducto,cantidad,subtotal,precioCompra")] LineaCompras lineaCompras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lineaCompras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra", lineaCompras.idCompra);
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaCompras.idProducto);
            return View(lineaCompras);
        }

        // GET: LineaCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaCompras lineaCompras = db.LineaCompras.Find(id);
            if (lineaCompras == null)
            {
                return HttpNotFound();
            }
            return View(lineaCompras);
        }

        // POST: LineaCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LineaCompras lineaCompras = db.LineaCompras.Find(id);
            db.LineaCompras.Remove(lineaCompras);
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
