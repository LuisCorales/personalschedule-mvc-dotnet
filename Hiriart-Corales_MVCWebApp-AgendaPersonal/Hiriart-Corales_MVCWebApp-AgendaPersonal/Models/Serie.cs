using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class Serie
    {
        public int SerieID { get; set; }
        public string Titulo { get; set; }

        public virtual ICollection<Evento> Evento { get; set; }
    }
}