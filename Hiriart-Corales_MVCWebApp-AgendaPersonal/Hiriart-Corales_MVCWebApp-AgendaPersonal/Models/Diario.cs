using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class Diario
    {
        public int DiarioID { get; set; }
        public List<Nullable<int>> ListaEventoID { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string Contenido { get; set; }
        
        public virtual ListaEvento ListaEvento { get; set; }
    }
}