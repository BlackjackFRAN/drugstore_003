//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace drugstore_003.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Compras
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Compras()
        {
            this.LineaCompras = new HashSet<LineaCompras>();
        }
    
        public int idCompra { get; set; }
        public Nullable<int> idProveedor { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<double> total { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
    
        public virtual Proveedors Proveedors { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LineaCompras> LineaCompras { get; set; }
    }
}
