using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class Evento
    {
        public int EventoID { get; set; }
        public int DiarioID { get; set; }
        public int SerieID { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }

        public virtual Diario Diario { get; set; }
        public virtual Serie Serie { get; set; }
        public virtual ICollection<Contacto> Contacto { get; set; }
        public virtual ICollection<Notificacion> Notificacion { get; set; }
        public virtual ICollection<Memo> Memo { get; set; }
    }
}