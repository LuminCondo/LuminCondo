//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GestionAsignacionPlanes
    {
        public int IDAsignacion { get; set; }
        public int IDResidencia { get; set; }
        public int IDPlan { get; set; }
        public System.DateTime fechaAsignacion { get; set; }
        public bool estadoPago { get; set; }
    
        public virtual GestionPlanCobros GestionPlanCobros { get; set; }
        public virtual GestionResidencias GestionResidencias { get; set; }
    }
}