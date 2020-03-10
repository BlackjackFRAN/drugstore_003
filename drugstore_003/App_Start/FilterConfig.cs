﻿using System.Web;
using System.Web.Mvc;

namespace drugstore_003
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filtros.VerificaSesion());
        }
    }
}
