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
    public class EmpleadoesController : Controller
    {
        private Bd_drugstore_002Entities db = new Bd_drugstore_002Entities();

        // GET: Empleadoes
        public ActionResult Index()
        {
            var empleadoes = db.Empleadoes.Include(e => e.Direccions);
            return View(empleadoes.ToList());
        }

        // GET: Empleadoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleadoes empleadoes = db.Empleadoes.Find(id);
            if (empleadoes == null)
            {
                return HttpNotFound();
            }
            return View(empleadoes);
        }

        // GET: Empleadoes/Create
        public ActionResult Create()
        {
            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle");
            return View();
        }

        // POST: Empleadoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmpleado,idDireccion,tipo,nombre,apellido,fechaNacimiento,sueldoBase,estadoCivil,dni")] Empleadoes empleadoes)
        {
            if (ModelState.IsValid)
            {
                db.Empleadoes.Add(empleadoes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", empleadoes.idDireccion);
            return View(empleadoes);
        }

        // GET: Empleadoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleadoes empleadoes = db.Empleadoes.Find(id);
            if (empleadoes == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", empleadoes.idDireccion);
            return View(empleadoes);
        }

        // POST: Empleadoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmpleado,idDireccion,tipo,nombre,apellido,fechaNacimiento,sueldoBase,estadoCivil,dni")] Empleadoes empleadoes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleadoes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", empleadoes.idDireccion);
            return View(empleadoes);
        }

        // GET: Empleadoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleadoes empleadoes = db.Empleadoes.Find(id);
            if (empleadoes == null)
            {
                return HttpNotFound();
            }
            return View(empleadoes);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleadoes empleadoes = db.Empleadoes.Find(id);
            db.Empleadoes.Remove(empleadoes);
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
