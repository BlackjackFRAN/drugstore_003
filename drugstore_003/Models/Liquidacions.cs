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
    
    public partial class Liquidacions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Liquidacions()
        {
            this.DetalleLiquidacions = new HashSet<DetalleLiquidacions>();
        }
    
        public int idLiquidacion { get; set; }
        public Nullable<int> idEmpleado { get; set; }
        public Nullable<int> anio { get; set; }
        public Nullable<int> mes { get; set; }
        public Nullable<System.DateTime> fechaDeposito { get; set; }
        public Nullable<double> totalNeto { get; set; }
        public Nullable<double> bruto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleLiquidacions> DetalleLiquidacions { get; set; }
        public virtual Empleadoes Empleadoes { get; set; }
    }
}
