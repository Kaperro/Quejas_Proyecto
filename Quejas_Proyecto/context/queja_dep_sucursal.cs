using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quejas_Proyecto.context
{
    public class queja_dep_sucursal
    {
        public comercio ComercioView { get; set; }
        public departamento DepaView { get; set; }
        public sucursal SucuView { get; set; }
        public queja QuejaView { get; set; }
    }
}