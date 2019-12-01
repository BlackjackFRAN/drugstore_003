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
    public class DetalleLiquidacionsController : Controller
    {
        private Bd_drugstore_002Entities db = new Bd_drugstore_002Entities();

        // GET: DetalleLiquidacions
        public ActionResult Index()
        {
            var detalleLiquidacions = db.DetalleLiquidacions.Include(d => d.Conceptoes).Include(d => d.Liquidacions);
            return View(detalleLiquidacions.ToList());
        }

        // GET: DetalleLiquidacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleLiquidacions detalleLiquidacions = db.DetalleLiquidacions.Find(id);
            if (detalleLiquidacions == null)
            {
                return HttpNotFound();
            }
            return View(detalleLiquidacions);
        }

        // GET: DetalleLiquidacions/Create
        public ActionResult Create()
        {
            ViewBag.idConcepto = new SelectList(db.Conceptoes, "idConcepto", "descripcion");
            ViewBag.idLiquidacion = new SelectList(db.Liquidacions, "idLiquidacion", "idLiquidacion");
            return View();
        }

        // POST: DetalleLiquidacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetalle,idLiquidacion,idConcepto,monto")] DetalleLiquidacions detalleLiquidacions)
        {
            if (ModelState.IsValid)
            {
                db.DetalleLiquidacions.Add(detalleLiquidacions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idConcepto = new SelectList(db.Conceptoes, "idConcepto", "descripcion", detalleLiquidacions.idConcepto);
            ViewBag.idLiquidacion = new SelectList(db.Liquidacions, "idLiquidacion", "idLiquidacion", detalleLiquidacions.idLiquidacion);
            return View(detalleLiquidacions);
        }

        // GET: DetalleLiquidacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleLiquidacions detalleLiquidacions = db.DetalleLiquidacions.Find(id);
            if (detalleLiquidacions == null)
            {
                return HttpNotFound();
            }
            ViewBag.idConcepto = new SelectList(db.Conceptoes, "idConcepto", "descripcion", detalleLiquidacions.idConcepto);
            ViewBag.idLiquidacion = new SelectList(db.Liquidacions, "idLiquidacion", "idLiquidacion", detalleLiquidacions.idLiquidacion);
            return View(detalleLiquidacions);
        }

        // POST: DetalleLiquidacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDetalle,idLiquidacion,idConcepto,monto")] DetalleLiquidacions detalleLiquidacions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleLiquidacions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idConcepto = new SelectList(db.Conceptoes, "idConcepto", "descripcion", detalleLiquidacions.idConcepto);
            ViewBag.idLiquidacion = new SelectList(db.Liquidacions, "idLiquidacion", "idLiquidacion", detalleLiquidacions.idLiquidacion);
            return View(detalleLiquidacions);
        }

        // GET: DetalleLiquidacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleLiquidacions detalleLiquidacions = db.DetalleLiquidacions.Find(id);
            if (detalleLiquidacions == null)
            {
                return HttpNotFound();
            }
            return View(detalleLiquidacions);
        }

        // POST: DetalleLiquidacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleLiquidacions detalleLiquidacions = db.DetalleLiquidacions.Find(id);
            db.DetalleLiquidacions.Remove(detalleLiquidacions);
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
