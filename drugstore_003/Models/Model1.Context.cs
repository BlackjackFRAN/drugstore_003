﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Bd_drugstore_002Entities : DbContext
    {
        public Bd_drugstore_002Entities()
            : base("name=Bd_drugstore_002Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Compras> Compras { get; set; }
        public virtual DbSet<Conceptoes> Conceptoes { get; set; }
        public virtual DbSet<DetalleLiquidacions> DetalleLiquidacions { get; set; }
        public virtual DbSet<Direccions> Direccions { get; set; }
        public virtual DbSet<Empleadoes> Empleadoes { get; set; }
        public virtual DbSet<Familiars> Familiars { get; set; }
        public virtual DbSet<LineaCompras> LineaCompras { get; set; }
        public virtual DbSet<LineaVentas> LineaVentas { get; set; }
        public virtual DbSet<Liquidacions> Liquidacions { get; set; }
        public virtual DbSet<Localidads> Localidads { get; set; }
        public virtual DbSet<Productoes> Productoes { get; set; }
        public virtual DbSet<Proveedors> Proveedors { get; set; }
        public virtual DbSet<Provincias> Provincias { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Ventas> Ventas { get; set; }
    }
}