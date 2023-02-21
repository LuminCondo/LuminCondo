using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class TiposUsuariosMetadata
    {
        [Display(Name = "ID de Tipo de Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int ID { get; set; }

        [Display(Name = "Tipo de Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string TipoUsuario { get; set; }
    }
    internal partial class UsuariosMetadata
    {
        [Display(Name = "ID del Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
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
        [Display(Name = "Telefono del Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int telefono { get; set; }
    }
    internal partial class EspaciosMetadata
    {
        [Display(Name = "Numero del Espacio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDEspacio { get; set; }
        [Display(Name = "Descripcion del Espacio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
    }
    internal partial class GestionReservasMetadata
    {
        [Display(Name = "Numero de Reserva")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDReserva { get; set; }
        [Display(Name = "Identificacion de Usuario")]
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
        [Display(Name = "Numero de Tipo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IdTipoInfo { get; set; }

        [Display(Name = "Tipo de Informacion")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string estado { get; set; }
    }
    internal partial class InformacionMetadata
    {
        [Display(Name = "Numero de Informacion")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDinformacion { get; set; }

        [Display(Name = "Numero de Tipo de Informacion")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string IdTipoInfo { get; set; }
        [Display(Name = "Fecha de la informacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime fechaPublicacion { get; set; }
        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string titulo { get; set; }
        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
        [Display(Name = "Foto")]
        public byte[] foto { get; set; }
    }
    internal partial class EstadoIncidenciaMetadata
    {
        [Display(Name = "Numero de Estado de la Incidencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDEstado { get; set; }
        [Display(Name = "Estado de la Incidencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string TipoEstado { get; set; }
    }
    internal partial class ReporteIncidenciasMetadata
    {
        [Display(Name = "Numero de la Incidencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDIncidencia { get; set; }
        [Display(Name = "Numero de Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDEstado { get; set; }
        [Display(Name = "Estado de la Incidencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDUsuario { get; set; }
        [Display(Name = "Estado de la Incidencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
    }
    internal partial class EstadoResidenciaMetadata
    {
        [Display(Name = "Numero de Estado de la Residencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IdEstadoResidencia { get; set; }

        [Display(Name = "Estado de la Residencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string estado { get; set; }
    }

    internal partial class GestionResidenciasMetadata
    {
        [Display(Name = "Numero de la Residencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDResidencia { get; set; }
        [Display(Name = "Idetificacion del Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDUsuario { get; set; }
        [Display(Name = "Cantidad de Personas")]
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
        [Display(Name = "Numero de Placa del Carro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
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
        [Display(Name = "Cedula")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDCedula { get; set; }
        [Display(Name = "Numero de Residencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDResidencia { get; set; }
        [Display(Name = "Nombre de la persona")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string nombre { get; set;}
    }

    internal partial class PlanesMetadata
    {
        [Display(Name = "Numero de Plan")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDPlan { get; set; }

        [Display(Name = "Descripcion del Plan")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
    }

    internal partial class GestionRubrosCobrosMetadata
    {
        [Display(Name = "Numero de Rubro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDRubro { get; set; }
        [Display(Name = "Descripcion del Rubro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string descripcion { get; set; }
        [Display(Name = "Monto del Rubro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "{0} acepta solo números con dos decimales")]
        public decimal monto { get; set;}
    }

    internal partial class GestionAsignacionPlanesMetadata
    {
        [Display(Name = "Numero de Asignacion")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDAsignacion { get; set; }
        [Display(Name = "Numero de Residencia")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDResidencia { get; set; }
        [Display(Name = "Numero de Plan")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IDPlan { get; set; }
        [Display(Name = "Fecha de asignacion")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime fechaAsignacion { get; set; }
        [Display(Name = "Estado de Pago")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public bool estadoPago { get; set; }
    }




}
