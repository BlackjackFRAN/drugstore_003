using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace drugstore_003.Models
{
    public class VentaCLS
    {

        public int idVenta { get; set; }
        public int idUsuario { get; set; }
        [DataType(DataType.Date)]
        public DateTime fecha { get; set; }
        public string total { get; set; }

        public List<LineaVentasCLS> LineaVentas { get; set; }

        public int idProducto { get; set; }
        public string descripcion { get; set; }
        public Nullable<double> precioVenta { get; set; }
        public int cantidad { get; set; }


    }
}
