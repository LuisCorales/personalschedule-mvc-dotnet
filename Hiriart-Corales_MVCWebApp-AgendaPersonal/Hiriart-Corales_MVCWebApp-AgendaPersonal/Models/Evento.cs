using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class Evento
    {
        public int EventoID { get; set; }      
        public Nullable<int> NotificacionID { get; set; }
        public Nullable<int> MemoID { get; set; }
        public Nullable<int> ListaContactoID { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public DateTime Inicio { get; set; }
        [Required]
        public DateTime Fin { get; set; }
        [Required]
        [StringLength(40)]
        public string Titulo { get; set; }
        [StringLength(140)]
        public string Descripcion { get; set; }
        [StringLength(100)]
        public string Ubicacion { get; set; }
        public bool EsSerie { get; set; }
        public string Dias { get; set; }

        public virtual ListaContacto ListaContacto { get; set; }
        public virtual Notificacion Notificacion { get; set; }
        public virtual Memo Memo { get; set; }
    }
}