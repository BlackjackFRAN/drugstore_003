using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using drugstore_003.Models;

namespace drugstore_003.Filtros
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AutorizarSesion : AuthorizeAttribute
    {
        private Usuarios oUsuarios;
        private Bd_drugstore_002Entities db = new Bd_drugstore_002Entities();
        private int idCategoria;

        public AutorizarSesion(int idCategoria = 0)
        {
            this.idCategoria = idCategoria;
        }

        public void OnAthorization(AuthorizationContext filterContext)
        {
            String nombreOperacion = "";
            String nombreModulo = "";
            try
            {
                oUsuarios = (Usuarios) HttpContext.Current.Session["user"];
                var categoria = from m in db.Usuarios
                    where m.usuario1 == oUsuarios.usuario1 && m.contrasenia == oUsuarios.contrasenia
                    select m;
                if (categoria.ToList().Count() == 0)
                {
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorExcepcion=");
                }
            }
            catch (Exception e)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorExcepcion=" + e.Message);
            }
        }
    }
}