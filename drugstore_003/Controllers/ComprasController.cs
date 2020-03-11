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

            /*
            List<LineaCompraCLS> listaLineas = null;
            using (var bd2 = new Bd_drugstore_002Entities())
            {
                listaLineas = (from LineaCompras in bd2.LineaCompras
                               where LineaCompras.idCompra == id
                               join Productoes in bd2.Productoes
                               on LineaCompras.idProducto equals Productoes.idProducto
                               select new LineaCompraCLS
                               {
                                   idLineaVenta = LineaCompras.idLineaCompra,
                                   idProducto = LineaCompras.idProducto,
                                   cantidad = LineaCompras.cantidad,
                                   subtotal = LineaCompras.subtotal,
                                   precio = LineaCompras.precioCompra,
                                   descripcion = Productoes.descripcion
                               }).ToList();
            }
            

            List<LineaCompras> lista = db.LineaCompras.Find()

            ViewBag.ListaDetalles = listaLineas;*/
            return View(compras);
        }

        public void cargarProducto()
        {
            List<Productoes> lista = db.Productoes.ToList();
            ViewBag.ListaProducto = new SelectList(lista, "idProducto", "descripcion");
        }

        public void cargarProveedores()
        {
            List<Proveedors> lista = db.Proveedors.ToList();
            ViewBag.ListaProveedores = new SelectList(lista, "idProveedor", "nombre");
        }


        // GET: Compras/Create
        public ActionResult Create()
        {
            //ViewBag.idProveedor = new SelectList(db.Proveedors, "idProveedor", "nombre");
            ViewBag.idUsuario = new SelectList(db.Usuarios, "idUsuario", "usuario1");
            cargarProducto();
            cargarProveedores();
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
        public ActionResult GuardarCompra(float Total, List<LineaVentas> ListadoDetalle)
        {

            string mensaje = "recibimos el mensaje.. el total: " + Total;

            if (Total == 0)
            {
                mensaje = "ERROR EN EL CAMPO COMPRA";
            }
            else
            {
                //REGISTRO DE VENTA
                Compras compra = new Compras
                {
                    fecha = DateTime.Now,
                    idUsuario = 1,
                    idProveedor = 1,
                    total = Total
                };


                db.Compras.Add(compra);
                db.SaveChanges();


                //Ventas venta2 = db.Ventas.Find(venta.fecha);

                int idC = db.Compras.Max(c => c.idCompra);


                //from ventas in db.Ventas where ventas.fecha == venta.fecha; 


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

                            idCompra = idC
                        };
                        db.LineaCompras.Add(lc);
                        db.SaveChanges();

                    }
                    mensaje = "COMPRA GUARDADA CON EXITO";
                }
            }

            return Json(mensaje);
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


/*
 
    <div>
    <h4>Compras</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(model => model.total)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.total)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.fecha)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.fecha)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.Proveedors.nombre)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.Proveedors.nombre)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.Usuarios.usuario1)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.Usuarios.usuario1)
        </dd>

    </dl>
</div>



    
<p>
    @Html.ActionLink("Edit", "Edit", new { id = Model.idCompra }) |
    @Html.ActionLink("Back to List", "Index")
</p>
     */
