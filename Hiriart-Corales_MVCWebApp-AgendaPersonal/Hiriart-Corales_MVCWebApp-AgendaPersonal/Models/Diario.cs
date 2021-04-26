using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class Diario
    {
        public int DiarioID { get; set; }
        public DateTime Fecha { get; set; }
        public string Contenido { get; set; }
        public virtual ICollection<Evento> Evento { get; set; }
        //public virtual ICollection<Contacto> Contacto { get; set; }
    }
}