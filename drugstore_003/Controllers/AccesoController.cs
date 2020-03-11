using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace drugstore_003.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            try
            {
                using (Models.Bd_drugstore_002Entities db = new Models.Bd_drugstore_002Entities())
                {
                    var oUser = (from d in db.Usuarios
                        where d.usuario1 == user.Trim() && d.contrasenia == pass.Trim()
                        select d).FirstOrDefault();
                    if (oUser==null)
                    {
                        ViewBag.Error = "Usuario o Contraseña incorrectos";
                        return View();
                    }

                    Session["user"] = oUser;
                    Session["categoria"] = oUser.tipo;
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
                
            }
        }
    }
}