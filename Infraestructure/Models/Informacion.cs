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
    
    public partial class Informacion
    {
        public int IDInformacion { get; set; }
        public int IDTipoInfo { get; set; }
        public System.DateTime fechaPublicacion { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public byte[] foto { get; set; }
    
        public virtual TipoInformacion TipoInformacion { get; set; }
    }
}
