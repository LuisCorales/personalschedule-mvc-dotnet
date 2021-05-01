using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class ListaEvento
    {
        public int ListaEventoID { get; set; }
        public Nullable<int> IDDiario { get; set; }
        public int IDEvento { get; set; }

        public virtual ICollection<Diario> Diario { get; set; }
    }
}