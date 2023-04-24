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
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(GestionPlanCobrosMetadata))]
    public partial class GestionPlanCobros
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GestionPlanCobros()
        {
            this.GestionAsignacionPlanes = new HashSet<GestionAsignacionPlanes>();
            this.GestionRubrosCobros = new HashSet<GestionRubrosCobros>();
        }
    
        public int IDPlan { get; set; }
        public string descripcion { get; set; }
        public decimal total { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GestionAsignacionPlanes> GestionAsignacionPlanes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GestionRubrosCobros> GestionRubrosCobros { get; set; }
    }
}
