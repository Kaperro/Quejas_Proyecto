using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quejas_Proyecto.context
{
    public class Fechas
    {
        [Display(Name = "Fecha inicio")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Fecha_queja_inicio { get; set; }

        [Display(Name = "Fecha Fin")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Fecha_queja_Fin { get; set; }

    }
}