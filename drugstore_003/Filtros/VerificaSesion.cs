using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using drugstore_003.Controllers;
using drugstore_003.Models;

namespace drugstore_003.Filtros
{
    public class VerificaSesion : ActionFilterAttribute
    {
        private Usuarios oUsuario;

        public override void OnActionExecuting(ActionExecutingContext filteContext)
        {
            try
            {
                base.OnActionExecuting(filteContext);
                oUsuario = (Usuarios) HttpContext.Current.Session["user"];
                if (oUsuario==null)
                {
                    if (filteContext.Controller is AccesoController == false)
                    {
                        filteContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }
                }
            }
            catch (Exception)
            {
               filteContext.Result = new RedirectResult("~/Acceso/Login");
            }
        }
    }
}