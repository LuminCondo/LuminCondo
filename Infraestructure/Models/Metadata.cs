using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class PlanesMetadata
    {
        [Display(Name = "IDPlan")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IdPlan { get; set; }

        [Display(Name = "descripcion")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }

        [Display(Name = "Rubros")]
        public virtual ICollection<GestionRubrosCobros> Rubros { get; set; }
    }
}
