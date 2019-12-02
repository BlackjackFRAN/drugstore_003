using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace drugstore_003.Models
{
    public class EmpleadoCLS
    {
        public int idEmpleado { get; set; }
        public Nullable<int> idDireccion { get; set; }
        public Nullable<int> tipo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> fechaNacimiento { get; set; }
        public Nullable<double> sueldoBase { get; set; }
        public string estadoCivil { get; set; }
        public Nullable<int> dni { get; set; }

        //// ----- PROPIEDADES ADICIONALES ----- ////

        public string calle { get; set; }
        public Nullable<int> numero { get; set; }

        public Nullable<int> codigoPostal { get; set; }
        public string nombreLocalidad { get; set; }
        public int idProvincia { get; set; }
        public string nombreProvincia { get; set; }
    }
}