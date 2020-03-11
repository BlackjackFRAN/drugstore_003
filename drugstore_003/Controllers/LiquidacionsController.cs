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
    public class LiquidacionsController : Controller
    {
        private Bd_drugstore_002Entities db = new Bd_drugstore_002Entities();

        // GET: Liquidacions
        public ActionResult Index()
        {
            var liquidacions = db.Liquidacions.Include(l => l.Empleadoes);
            return View(liquidacions.ToList());
        }

        // GET: Liquidacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidacions liquidacions = db.Liquidacions.Find(id);
            if (liquidacions == null)
            {
                return HttpNotFound();
            }
            return View(liquidacions);
        }

        // GET: Liquidacions/Create
        public ActionResult Create()
        {
            //ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre");
            //CargarEmpleados();
            List<string> Student = new List<string>();
            Student.Add("Jignesh");
            Student.Add("Tejas");
            Student.Add("Rakesh");

            ViewBag.Student = Student;
            return View();
        }

        public void CargarEmpleados() {
            List<Empleadoes> lista = db.Empleadoes.ToList();
            ViewBag.ListaEmpleado = lista;
        }

        // POST: Liquidacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLiquidacion,idEmpleado,anio,mes,fechaDeposito,totalNeto,bruto")] Liquidacions liquidacions)
        {
            if (ModelState.IsValid)
            {
                db.Liquidacions.Add(liquidacions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre", liquidacions.idEmpleado);
            return View(liquidacions);
        }

        // GET: Liquidacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidacions liquidacions = db.Liquidacions.Find(id);
            if (liquidacions == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre", liquidacions.idEmpleado);
            return View(liquidacions);
        }

        public ActionResult Liquidacion()
        {
            List<Empleadoes> lista = db.Empleadoes.ToList();
            ViewBag.ListaEmpleado = lista;
            return View();
        }

        
        [HttpPost]
        public ActionResult LiquidarEmpleado (int idEmpleado, DateTime mes)
        {
            string mensaje="id Empleado: "+idEmpleado +"Mes: "+ mes;
            return Json(mensaje);            
        }
        
        // POST: Liquidacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLiquidacion,idEmpleado,anio,mes,fechaDeposito,totalNeto,bruto")] Liquidacions liquidacions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liquidacions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre", liquidacions.idEmpleado);
            return View(liquidacions);
        }

        // GET: Liquidacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidacions liquidacions = db.Liquidacions.Find(id);
            if (liquidacions == null)
            {
                return HttpNotFound();
            }
            return View(liquidacions);
        }

        // POST: Liquidacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Liquidacions liquidacions = db.Liquidacions.Find(id);
            db.Liquidacions.Remove(liquidacions);
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
