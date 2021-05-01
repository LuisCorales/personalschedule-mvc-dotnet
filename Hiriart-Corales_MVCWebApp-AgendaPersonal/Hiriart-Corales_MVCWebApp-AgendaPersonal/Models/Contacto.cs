using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class Contacto
    {
        public int ContactoID { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(30)]
        public string Apellido { get; set; }
        [StringLength(30)]
        public string Telefono { get; set; }
        [StringLength(60)]
        public string Email { get; set; }
        [StringLength(60)]
        public string Organizacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [StringLength(140)]
        public string InformacionAdicional { get; set; }

        public virtual ICollection<Evento> Evento { get; set; }
    }
}