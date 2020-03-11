using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace drugstore_003.Models
{
    public class LineaCompraCLS
    {
        public int idLineaVenta { get; set; }
        public Nullable<int> idCompra { get; set; }
        public Nullable<int> idProducto { get; set; }
        public Nullable<int> cantidad { get; set; }
        public Nullable<double> subtotal { get; set; }
        public Nullable<double> precio { get; set; }


        /// ----  PROPIEDADES ADICIONALES ---- ///
        public string descripcion { get; set; }
    }
}