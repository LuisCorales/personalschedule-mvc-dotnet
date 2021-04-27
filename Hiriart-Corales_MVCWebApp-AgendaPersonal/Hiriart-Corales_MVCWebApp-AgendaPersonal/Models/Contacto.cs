using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class Contacto
    {
        public int ContactoID { get; set; }
        public Nullable<int> EventoID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Organizacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string InformacionAdicional { get; set; }

        public virtual Evento Evento { get; set; }
    }
}