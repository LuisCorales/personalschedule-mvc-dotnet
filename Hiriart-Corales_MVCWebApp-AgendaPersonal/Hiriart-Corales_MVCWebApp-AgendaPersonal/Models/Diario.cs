using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class Diario
    {
        public int DiarioID { get; set; }
        public Nullable<int> ListaEventoID { get; set; }
        public DateTime Fecha { get; set; }
        public string Contenido { get; set; }
        
        public virtual ListaEvento ListaEvento { get; set; }
    }
}