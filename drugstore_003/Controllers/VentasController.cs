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
    public class VentasController : Controller
    {
        private Bd_drugstore_002Entities db = new Bd_drugstore_002Entities();

        // GET: Ventas
        public ActionResult Index()
        {
            return View(db.Ventas.ToList());
        }

        // GET: Ventas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return HttpNotFound();
            }

            List<LineaVentasCLS> listaLineas = null;
            using (var bd2 = new Bd_drugstore_002Entities())
            {
                listaLineas = (from LineaVentas in bd2.LineaVentas
                               where LineaVentas.idVenta == id
                               join Productoes in bd2.Productoes
                               on LineaVentas.idProducto equals Productoes.idProducto
                               select new LineaVentasCLS
                               {
                                   idLineaVenta = LineaVentas.idLineaVenta,
                                   idProducto = LineaVentas.idProducto,
                                   cantidad = LineaVentas.cantidad,
                                   subtotal = LineaVentas.subtotal,
                                   precio = LineaVentas.precio,
                                   descripcion = Productoes.descripcion
                               }).ToList();
            }

            VentaCLS venta2 = new VentaCLS();
            venta2.idVenta = ventas.idVenta;
            venta2.idUsuario = (int)ventas.idUsuario;
            venta2.fecha = (DateTime)ventas.fecha;
            venta2.total = ventas.total.ToString();
            venta2.LineaVentas = listaLineas;

            return View(venta2);
        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            return View();
        }

        public void cargarProducto()
        {
            List<Productoes> lista = db.Productoes.ToList();
            ViewBag.ListaProducto = new SelectList(lista, "idProducto", "descripcion");
        }

        public ActionResult NuevaVenta()
        {
            /*
            VentaCLS venta = new VentaCLS();
            venta.idUsuario = 1;
            venta.fecha = DateTime.Now.Date;
            List<LineaVentasCLS> lista = new List<LineaVentasCLS>();
            venta.LineaVentas = lista;
            return View(venta);
            */

            cargarProducto();
            return View();

        }

        [HttpPost]
        public ActionResult NuevaVenta(VentaCLS venta)
        {
            //  Productoes producto = db.Productoes.Find(venta.idProducto);

            /*
                LineaVentasCLS linea = new LineaVentasCLS
                {
                    idProducto = producto.idProducto,
                    descripcion = producto.descripcion,
                    precio = producto.precioVenta,
                    cantidad = venta.cantidad,
                    subtotal = venta.cantidad * producto.precioVenta
                };
           */
            //venta.LineaVentas.Add(linea);

            //venta.idProducto = producto.idProducto;
            VentaCLS venta2 = new VentaCLS();
            venta2 = venta;
            venta2.descripcion = "listo wey";
            venta2.precioVenta = 50;
            //venta.cantidad = 0;

            return View(venta2);
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
        public ActionResult GuardarVenta(string Total, List<LineaVentas> ListadoDetalle)
        {

            string mensaje = "recibimos el mensaje.. el total: " + Total;

            if (Total == "")
            {
                mensaje = "ERROR EN EL CAMPO TOTAL";
            }
            else
            {
                //REGISTRO DE VENTA
                Ventas venta = new Ventas
                {
                    fecha = DateTime.Now,
                    idUsuario = 1,
                    total = Total,
                };


                db.Ventas.Add(venta);
                db.SaveChanges();


                //Ventas venta2 = db.Ventas.Find(venta.fecha);

                int idV = db.Ventas.Max(p => p.idVenta);


                //from ventas in db.Ventas where ventas.fecha == venta.fecha; 


                if (idV == 0)
                {
                    mensaje = "ERROR AL REGISTRAR LA VENTA";
                }
                else
                {
                    foreach (var data in ListadoDetalle)
                    {
                        LineaVentas lv = new LineaVentas
                        {
                            idProducto = data.idProducto,
                            cantidad = data.cantidad,
                            subtotal = data.subtotal,
                            idVenta = idV
                        };
                        db.LineaVentas.Add(lv);
                        db.SaveChanges();

                    }
                    mensaje = "VENTA GUARDADA CON EXITO";
                }
            }

            return Json(mensaje);
        }


        // POST: Ventas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVenta,idUsuario,fecha,total")] Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                db.Ventas.Add(ventas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ventas);
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return HttpNotFound();
            }
            return View(ventas);
        }

        // POST: Ventas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVenta,idUsuario,fecha,total")] Ventas ventas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ventas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ventas);
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return HttpNotFound();
            }
            return View(ventas);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ventas ventas = db.Ventas.Find(id);
            db.Ventas.Remove(ventas);
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

        public ActionResult Reporte()
        {
            return View();
        }
    
        
        public ActionResult Imprimir()
        {
   

            return new ViewAsPdf("Report")
            {
                FileName = "Venta.pdf",
   
            };
        }
    }
}


/* @model drugstore_003.Models.VentaCLS
    @{
    ViewBag.Title = "NuevaVenta";
}
@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()
    <div class="form-horizontal">
        <h4>Ventas</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        <div class="form-group">
            @Html.LabelFor(model => model.idUsuario, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.idUsuario, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.idUsuario, "", new { @class = "text-danger" })
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(model => model.fecha, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.fecha, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.fecha, "", new { @class = "text-danger" })
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(model => model.idProducto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.idProducto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.idProducto, "", new { @class = "text-danger" })
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(model => model.descripcion, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.descripcion, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.descripcion, "", new { @class = "text-danger" })
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(model => model.precioVenta, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.precioVenta, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.precioVenta, "", new { @class = "text-danger" })
            </div>
        </div>
        <div class="form-group">
            @Html.LabelFor(model => model.cantidad, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.cantidad, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.cantidad, "", new { @class = "text-danger" })
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="NuevaVenta" class="btn btn-default" />
            </div>
        </div>
        <table class="table">
            <tr>
                <th>
                    idProducto
                </th>
                <th>
                    Descripcion
                </th>
                <th>
                    precio
                </th>
                <th>
                    cantidad
                </th>
                <th>
                    subtotal
                </th>
                <th></th>
            </tr>
        </table>
        <div class="form-group">
            @Html.DisplayNameFor(model => model.total)
            @Html.DisplayFor(model => model.idUsuario)
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Create" class="btn btn-default" />
            </div>
        </div>
    </div>
}
<div>
    @Html.ActionLink("Regresar", "Index")
</div>
    */
