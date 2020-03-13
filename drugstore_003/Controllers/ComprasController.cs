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
    public class ComprasController : Controller
    {
        private Bd_drugstore_002Entities db = new Bd_drugstore_002Entities();

        // GET: Compras
        public ActionResult Index()
        {
            var compras = db.Compras.Include(c => c.Proveedors).Include(c => c.Usuarios);
            return View(compras.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            List<LineaCompras> lista = db.LineaCompras.Where(l => l.idCompra == compras.idCompra).ToList();
            List<LineaComprasCLS> lista2= new List<LineaComprasCLS>();

            foreach(var linea in lista)
            {
                LineaComprasCLS linea2 = new LineaComprasCLS();
                linea2.idProducto = linea.idProducto;
                Productoes pro = db.Productoes.Find(linea.idProducto);
                linea2.descripcion = pro.descripcion;
                linea2.cantidad = linea.cantidad;
                linea2.precioCompra = linea.precioCompra;
                linea2.subtotal = linea.subtotal;
                lista2.Add(linea2);
            }

            ViewBag.ListaDetalle = lista2;
            return View(compras);
        }

        /*
          // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }
             */

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.idProveedor = new SelectList(db.Proveedors, "idProveedor", "nombre");
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "usuario1");
            return View();
        }

        // POST: Compras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCompra,idProveedor,idUsuario,total,fecha")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Compras.Add(compras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProveedor = new SelectList(db.Proveedors, "idProveedor", "nombre", compras.idProveedor);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "usuario1", compras.idUsuario);
            return View(compras);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProveedor = new SelectList(db.Proveedors, "idProveedor", "nombre", compras.idProveedor);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "usuario1", compras.idUsuario);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCompra,idProveedor,idUsuario,total,fecha")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProveedor = new SelectList(db.Proveedors, "idProveedor", "nombre", compras.idProveedor);
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "usuario1", compras.idUsuario);
            return View(compras);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return HttpNotFound();
            }
            return View(compras);
        }

        public void cargarProducto()
        {
            List<Productoes> lista = db.Productoes.ToList();
            ViewBag.ListaProducto = new SelectList(lista, "idProducto", "descripcion");
        }

        public void cargarProveedor()
        {
            List<Proveedors> lista = db.Proveedors.ToList();
            ViewBag.ListaProveedor = new SelectList(lista, "idProveedor", "nombre");
        }

        public ActionResult NuevaCompra()
        {
            cargarProducto();
            cargarProveedor();
            return View();

        }

        [HttpPost]
        public ActionResult Seleccionar(int idProducto)
        {
            Productoes pro = new Productoes(idProducto);
            Productoes pro2 = db.Productoes.Find(idProducto);
            if (pro2 == null)
            {
                return Json(pro, JsonRequestBehavior.AllowGet);
            }
            pro.idProducto = pro2.idProducto;
            pro.descripcion = pro2.descripcion;
            pro.precioVenta = pro2.precioVenta;
            return Json(pro, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GuardarVenta(double Total, int idProveedor, List<LineaVentas> ListadoDetalle)
        {

            string mensaje = "recibimos el mensaje.. el total: " + Total;

            if (Total == 0)
            {
                mensaje = "ERROR EN EL CAMPO TOTAL";
            }
            else
            {
                //REGISTRO DE VENTA
                Compras compra = new Compras()
                {
                    fecha = DateTime.Now,
                    idUsuario = 1,
                    idProveedor = idProveedor,
                    total = Total,
                };


                db.Compras.Add(compra);
                db.SaveChanges();


                int idC = db.Compras.Max(p => p.idCompra);

                if (idC == 0)
                {
                    mensaje = "ERROR AL REGISTRAR LA COMPRA";
                }
                else
                {
                    foreach (var data in ListadoDetalle)
                    {
                        LineaCompras lc = new LineaCompras
                        {
                            idProducto = data.idProducto,
                            cantidad = data.cantidad,
                            subtotal = data.subtotal,
                            precioCompra = data.precio,
                            idCompra = idC
                        };
                        Productoes pro = db.Productoes.Find(data.idProducto);
                        pro.stock += data.cantidad;
                        db.LineaCompras.Add(lc);
                        db.SaveChanges();
                    }
                    mensaje = "COMPRA GUARDADA CON EXITO";
                }
            }

            return Json(mensaje);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compras compras = db.Compras.Find(id);
            db.Compras.Remove(compras);
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
