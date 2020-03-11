using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace drugstore_003.Models
{
    public class CompraCLS
    {

        public int idCompra { get; set; }
        public Nullable<int> idProveedor { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<double> total { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> fecha { get; set; }

        public virtual Proveedors Proveedors { get; set; }
        public virtual Usuarios Usuarios { get; set; }


        public List<LineaComprasCLS> lineaCompras { get; set; }

        public int idProducto { get; set; }
        public string descripcion { get; set; }
        public Nullable<double> precioCompra { get; set; }
        public int cantidad { get; set; }

        public CompraCLS(int idCompra, int? idProveedor, int? idUsuario, double? total, DateTime? fecha, Proveedors proveedors, Usuarios usuarios)
        {
            this.idCompra = idCompra;
            this.idProveedor = idProveedor;
            this.idUsuario = idUsuario;
            this.total = total;
            this.fecha = fecha;
            Proveedors = proveedors;
            Usuarios = usuarios;
        }
    }
}