//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            this.GestionReservas = new HashSet<GestionReservas>();
            this.GestionResidencias = new HashSet<GestionResidencias>();
            this.ReporteIncidencias = new HashSet<ReporteIncidencias>();
        }
    
        public int ID { get; set; }
        public string nombre { get; set; }
        public string contrasenna { get; set; }
        public int IDTipoUsuario { get; set; }
        public string email { get; set; }
        public bool estado { get; set; }
        public int telefono { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GestionReservas> GestionReservas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GestionResidencias> GestionResidencias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReporteIncidencias> ReporteIncidencias { get; set; }
        public virtual TiposUsuarios TiposUsuarios { get; set; }
    }
}
