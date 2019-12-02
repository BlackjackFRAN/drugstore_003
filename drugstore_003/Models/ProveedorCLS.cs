using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace drugstore_003.Models
{
    public class ProveedorCLS
    {
        public int idProveedor { get; set; }
        public Nullable<int> idDireccion { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public Nullable<int> cuit { get; set; }

        //// ----- PROPIEDADES ADICIONALES ----- ////

        public string calle { get; set; }
        public Nullable<int> numero { get; set; }

        public Nullable<int> codigoPostal { get; set; }
        public string nombreLocalidad { get; set; }
        public int idProvincia { get; set; }
        public string nombreProvincia { get; set; }

    }
}