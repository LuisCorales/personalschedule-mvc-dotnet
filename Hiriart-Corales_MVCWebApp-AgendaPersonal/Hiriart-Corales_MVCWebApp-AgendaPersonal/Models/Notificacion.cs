using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class Notificacion
    {
        public int NotificacionID { get; set; }     
        public string Titulo { get; set; }
        public DateTime Hora { get; set; }

        public virtual ICollection<Evento> Evento { get; set; }
    }
}