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
    
    public partial class ReporteIncidencias
    {
        public int IDIncidencia { get; set; }
        public int IDEstado { get; set; }
        public int IDUsuario { get; set; }
        public string descripcion { get; set; }
    
        public virtual EstadoIncidencia EstadoIncidencia { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
