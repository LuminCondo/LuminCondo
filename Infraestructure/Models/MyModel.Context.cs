﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Lumincondo_DBEntities : DbContext
    {
        public Lumincondo_DBEntities()
            : base("name=Lumincondo_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Carros> Carros { get; set; }
        public virtual DbSet<Espacios> Espacios { get; set; }
        public virtual DbSet<EstadoIncidencia> EstadoIncidencia { get; set; }
        public virtual DbSet<EstadoResidencia> EstadoResidencia { get; set; }
        public virtual DbSet<GestionAsignacionPlanes> GestionAsignacionPlanes { get; set; }
        public virtual DbSet<GestionPlanCobros> GestionPlanCobros { get; set; }
        public virtual DbSet<GestionReservas> GestionReservas { get; set; }
        public virtual DbSet<GestionResidencias> GestionResidencias { get; set; }
        public virtual DbSet<GestionRubrosCobros> GestionRubrosCobros { get; set; }
        public virtual DbSet<Informacion> Informacion { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<ReporteIncidencias> ReporteIncidencias { get; set; }
        public virtual DbSet<TipoInformacion> TipoInformacion { get; set; }
        public virtual DbSet<TiposUsuarios> TiposUsuarios { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
