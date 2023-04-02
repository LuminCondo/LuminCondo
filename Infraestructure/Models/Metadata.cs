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
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El tipo debe tener al menos 4 caracteres.")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string tipoUsuario { get; set; }
    }
    internal partial class UsuariosMetadata
    {
        
        public int ID { get; set; }
        [Display(Name = "Nombre del Usuario")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El nombre debe tener al menos 4 caracteres.")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string nombre { get; set; }
        [Display(Name = "Contraseña del Usuario")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "La contraseña debe tener al menos 4 caracteres.")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string contrasenna { get; set; }
        [Display(Name = "ID de Tipo de Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDTipoUsuario { get; set; }
        [Display(Name = "Correo del Usuario")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} no tiene formato válido")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string email { get; set; }
        [Display(Name = "Estado del Usuario")]

        public bool estado { get; set; }
        [Display(Name = "Teléfono del Usuario")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} El numero de teléfono no lleva guiones ni espacios, Ej: 24242424")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int telefono { get; set; }
    }
    internal partial class EspaciosMetadata
    {
        
        public int IDEspacio { get; set; }
        [Display(Name = "Descripción del Espacio")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "La descripción debe tener al menos 4 caracteres.")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
    }
    internal partial class GestionReservasMetadata
    {

        [Display(Name = "ID de la Reserva")]
        public int IDReserva { get; set; }
        [Display(Name = "Identificación de Usuario")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDUsuario { get; set; }
        [Display(Name = "Numero del Espacio")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDEspacio { get; set; }
        [Display(Name = "Fecha de la Reserva")]
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
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El tipo de información debe tener al menos 4 caracteres.")]
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
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El titulo debe tener al menos 4 caracteres.")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string titulo { get; set; }
        [Display(Name = "Descripción")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "La descripción debe tener al menos 4 caracteres.")]
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
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El estado debe tener al menos 4 caracteres.")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string TipoEstado { get; set; }
    }
    public partial class ReporteIncidenciasMetadata
    {
        
        public int IDIncidencia { get; set; }
        [Display(Name = "Numero de Estado")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDEstado { get; set; }
        [Display(Name = "Usuario que reporta la Incidencia")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDUsuario { get; set; }
        [Display(Name = "Descripción de la Incidencia")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "La descripción debe tener al menos 4 caracteres.")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
    }
    internal partial class EstadoResidenciaMetadata
    {
       
        public int IDEstadoResidencia { get; set; }

        [Display(Name = "Estado de la Residencia")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El estado debe tener al menos 4 caracteres.")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string estado { get; set; }
    }

    internal partial class GestionResidenciasMetadata
    {
        [Display(Name = "Número de Residencia")]
        public int IDResidencia { get; set; }
        [Display(Name = "Usuario Propietario")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDUsuario { get; set; }
        [Display(Name = "Cantidad de Residentes")]
        public int cantPersonas { get; set; }
        [Display(Name = "ID del Estado de la Residencia")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDEstadoResidencia { get; set; }
        [Display(Name = "Año de Inicio")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int annoInicio { get; set; }
        [Display(Name = "Cantidad de Carros")]
        public int cantCarros { get; set; }
    }

    internal partial class CarrosMetadata
    {
        
        public string IDPlaca { get; set; }
        [Display(Name = "Numero de Residencia")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDResidencia { get; set; }
        [Display(Name = "Modelo del Carro")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El modelo debe tener al menos 4 caracteres.")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string modelo { get; set; }
        [Display(Name = "Tipo de Carro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public bool tipo { get; set; }
    }

    internal partial class PersonasMetadata
    {
        
        public int IDCedula { get; set; }
        [Display(Name = "Numero de Residencia")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} solo acepta números")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDResidencia { get; set; }
        [Display(Name = "Nombre de la persona")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "El nombre debe tener al menos 4 caracteres.")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string nombre { get; set;}
        [Display(Name = "Tipo de Persona")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public bool tipo { get; set; }
    }

    internal partial class GestionPlanCobrosMetadata
    {
        
        public int IDPlan { get; set; }

        [Display(Name = "Descripción del Plan")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "La descripción debe tener al menos 4 caracteres.")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
        [Display(Name = "Rubros a Cobrar")]
        public virtual ICollection<GestionRubrosCobros> GestionRubrosCobros { get; set; }


    }

    internal partial class GestionRubrosCobrosMetadata
    {
        public int IDRubro { get; set; }
        [Display(Name = "Descripción del Rubro")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "La descripción debe tener al menos 4 caracteres.")]
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
        [Display(Name = "Número de Plan")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDPlan { get; set; }
        [Display(Name = "Número de Residencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDResidencia { get; set; }
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
