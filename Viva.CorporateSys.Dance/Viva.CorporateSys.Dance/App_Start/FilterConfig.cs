﻿using System.Web;
using System.Web.Mvc;

namespace Viva.CorporateSys.Dance
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}