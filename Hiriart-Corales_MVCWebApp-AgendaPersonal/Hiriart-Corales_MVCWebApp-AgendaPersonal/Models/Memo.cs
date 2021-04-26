using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class Memo
    {
        public int MemoID { get; set; }
        public int EventoID { get; set; }
        public string Titulo { get; set; }

        public virtual Evento Evento { get; set; }
    }
}