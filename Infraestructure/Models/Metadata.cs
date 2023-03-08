using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class TiposUsuariosMetadata
    {
        
        public int ID { get; set; }

        [Display(Name = "Tipo de Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string tipoUsuario { get; set; }
    }
    internal partial class UsuariosMetadata
    {
        
        public int ID { get; set; }
        [Display(Name = "Nombre del Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string nombre { get; set; }
        [Display(Name = "Contraseña del Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string contrasenna { get; set; }
        [Display(Name = "ID de Tipo de Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDTipoUsuario { get; set; }
        [Display(Name = "Correo del Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string email { get; set; }
        [Display(Name = "Estado del Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public bool estado { get; set; }
        [Display(Name = "Teléfono del Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int telefono { get; set; }
    }
    internal partial class EspaciosMetadata
    {
        
        public int IDEspacio { get; set; }
        [Display(Name = "Descripción del Espacio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
    }
    internal partial class GestionReservasMetadata
    {
        
        public int IDReserva { get; set; }
        [Display(Name = "Identificación de Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDUsuario { get; set; }
        [Display(Name = "Numero del Espacio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDEspacio { get; set; }
        [Display(Name = "Numero de Reserva")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime fecha { get; set; }
        [Display(Name = "Estado de la reserva")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public bool estado { get; set; }
    }

        internal partial class TipoInformacionMetadata
    {
       
        public int IDTipoInfo { get; set; }

        [Display(Name = "Tipo de Información")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string tipoInfo { get; set; }
    }
    internal partial class InformacionMetadata
    {
        
        public int IDInformacion { get; set; }

        [Display(Name = "Numero de Tipo de Información")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string IDTipoInfo { get; set; }
        [Display(Name = "Fecha de la información")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime fechaPublicacion { get; set; }
        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string titulo { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
        [Display(Name = "Foto")]
        public byte[] foto { get; set; }

        [Display(Name = "Tipo de Información")]
        public virtual ICollection<TipoInformacion> TipoInformacion { get; set; }
    }
    internal partial class EstadoIncidenciaMetadata
    {
        
        public int IDEstado { get; set; }
        [Display(Name = "Estado de la Incidencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string TipoEstado { get; set; }
    }
    internal partial class ReporteIncidenciasMetadata
    {
        
        public int IDIncidencia { get; set; }
        [Display(Name = "Numero de Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDEstado { get; set; }
        [Display(Name = "Usuario que reporta la Incidencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDUsuario { get; set; }
        [Display(Name = "Descripción de la Incidencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
    }
    internal partial class EstadoResidenciaMetadata
    {
       
        public int IDEstadoResidencia { get; set; }

        [Display(Name = "Estado de la Residencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string estado { get; set; }
    }

    internal partial class GestionResidenciasMetadata
    {
        [Display(Name = "Número de Residencia")]
        public int IDResidencia { get; set; }
        [Display(Name = "Identificativo del Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDUsuario { get; set; }
        [Display(Name = "Cantidad de Residentes")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int cantPersonas { get; set; }
        [Display(Name = "ID del Estado de la Residencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDEstadoResidencia { get; set; }
        [Display(Name = "Año de Inicio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int annoInicio { get; set; }
        [Display(Name = "Cantidad de Carros")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int cantCarros { get; set; }
    }

    internal partial class CarrosMetadata
    {
        
        public string IDPlaca { get; set; }
        [Display(Name = "Numero de Residencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDResidencia { get; set; }
        [Display(Name = "Modelo del Carro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string modelo { get; set; }
    }

    internal partial class PersonasResidentesMetadata
    {
        
        public int IDCedula { get; set; }
        [Display(Name = "Numero de Residencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDResidencia { get; set; }
        [Display(Name = "Nombre de la persona")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string nombre { get; set;}
    }

    internal partial class GestionPlanCobrosMetadata
    {
        
        public int IDPlan { get; set; }

        [Display(Name = "Descripción del Plan")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
        [Display(Name = "Rubros a Cobrar")]
        public virtual ICollection<GestionRubrosCobros> GestionRubrosCobros { get; set; }


    }

    internal partial class GestionRubrosCobrosMetadata
    {
        public int IDRubro { get; set; }
        [Display(Name = "Descripción del Rubro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
        [Display(Name = "Monto del Rubro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "{0} acepta solo números con dos decimales")]
        public decimal monto { get; set;}
    }

    internal partial class GestionAsignacionPlanesMetadata
    {
        
        public int IDAsignacion { get; set; }
        [Display(Name = "Numero de Plan")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDPlan { get; set; }
        [Display(Name = "Fecha de asignación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime fechaAsignacion { get; set; }
        [Display(Name = "Estado de Pago")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public bool estadoPago { get; set; }
    }



}
