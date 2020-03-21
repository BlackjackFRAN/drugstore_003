using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using drugstore_003.Models;
using Rotativa;
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
            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre");
            return View();
        }

        public ActionResult NuevaLiquidacion()
        {
            List<Empleadoes> lista = db.Empleadoes.ToList();
            ViewBag.ListaEmpleado = lista;
            return View();
        }

        
        [HttpPost]
        public ActionResult LiquidarEmpleado(int idEmpleado, DateTime mes)
        {
            string mensaje = "No se pudo Guardar la Liquidacion";
            Empleadoes emp = db.Empleadoes.Find(idEmpleado);
            int mess, anio,idEmp;
            bool encontrado = false;
            foreach (var liquidacions in db.Liquidacions) {
                mess = Convert.ToInt32(liquidacions.mes);
                anio = Convert.ToInt32(liquidacions.anio);
                idEmp = Convert.ToInt32(liquidacions.idEmpleado);
                if (idEmp == idEmpleado && mess == mes.Month && anio == mes.Year)
                {
                    encontrado = true;
                    mensaje = "La Liquidacion para este Empleado para el mes y año ingresado ya existe";
                    return Json(mensaje);
                }
            }

                if (encontrado == false)
                {
                Liquidacions liq = new Liquidacions()
                {
                    totalNeto = emp.sueldoBase,
                    bruto = emp.sueldoBase,
                    mes = mes.Month,
                    anio = mes.Year,
                    idEmpleado = idEmpleado,
                        fechaDeposito = DateTime.Now,
                    };
                    db.Liquidacions.Add(liq);
                    int neto = Convert.ToInt32(emp.sueldoBase);
                    db.SaveChanges();

                    int idL = db.Liquidacions.Max(l => l.idLiquidacion);
                    if (idL != 0)
                    {
                        foreach (var concepto in db.Conceptoes)
                        {
                           DetalleLiquidacions detalle = new DetalleLiquidacions();
                            detalle.idLiquidacion = idL;
                            detalle.idConcepto = concepto.idConcepto;
                            if (concepto.porcentaje == null)
                            {
                                detalle.monto = concepto.total;
                           
                            }
                            else
                            {
                                concepto.total = (emp.sueldoBase * concepto.porcentaje) / 100;
                                detalle.monto = (liq.bruto * concepto.porcentaje) / 100;
                            }
                            neto -= Convert.ToInt32(concepto.total);
                            db.DetalleLiquidacions.Add(detalle);
                        }
                        Liquidacions liq2 = db.Liquidacions.Find(idL);
                        liq2.totalNeto = neto;
                        db.SaveChanges();
                        mensaje = "se guardo con exito";
                    }
                }
                         
            return Json(mensaje);
        }

        [HttpPost]
        public ActionResult LiquidarTodosEmpleado(DateTime mes)
        {
            string mensaje = "Ya existe liquidacion para este periodo";
            int contador = 0;
            int mess = mes.Month;
            int anio = mes.Year;
            int neto = 0;

            List<Empleadoes> listaEmp = db.Empleadoes.ToList();

            foreach (var emp in listaEmp)
            {
                Liquidacions listaLiq = db.Liquidacions.Where(x => x.idEmpleado == emp.idEmpleado && x.mes == mess && x.anio == anio).FirstOrDefault();

                if (listaLiq == null) {
                    Liquidacions liq = new Liquidacions()
                    {
                        totalNeto = emp.sueldoBase,
                        bruto = emp.sueldoBase,
                        mes = mes.Month,
                        anio = mes.Year,
                        idEmpleado = emp.idEmpleado,
                        fechaDeposito = DateTime.Now,
                    };
                    db.Liquidacions.Add(liq);
                    neto = Convert.ToInt32(emp.sueldoBase);
                    db.SaveChanges();

                    int idL = db.Liquidacions.Max(l => l.idLiquidacion);
                    if (idL != 0)
                    {
                        foreach (var concepto in db.Conceptoes)
                        {
                            DetalleLiquidacions detalle = new DetalleLiquidacions();
                            detalle.idLiquidacion = idL;
                            detalle.idConcepto = concepto.idConcepto;
                            if (concepto.porcentaje == null)
                            {
                                detalle.monto = concepto.total;
                            }
                            else
                            {
                                concepto.total = (emp.sueldoBase * concepto.porcentaje) / 100;
                                detalle.monto = (liq.bruto * concepto.porcentaje) / 100;
                            }
                            neto -= Convert.ToInt32(concepto.total);
                            db.DetalleLiquidacions.Add(detalle);
                        }
                        Liquidacions liq2 = db.Liquidacions.Find(idL);
                        liq2.totalNeto = neto;
                        db.SaveChanges();
                        contador++;
                        mensaje = "se registraron" +contador +" liquidaciones";
                    }

                }

            }
            return Json(mensaje);
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


        public ActionResult Reporte(int id)
        {
            Liquidacions l = db.Liquidacions.Find(id);

            return View(l);
        }


        public ActionResult Imprimir(int id)
        {
            
            Liquidacions l = db.Liquidacions.Find(id);

            return new ViewAsPdf("Reporte",l)
            {
                FileName = "ReciboDeSueldo_"+l.Empleadoes.apellido+"_"+l.Empleadoes.nombre+".pdf",
                PageSize = Rotativa.Options.Size.A4

            };
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
