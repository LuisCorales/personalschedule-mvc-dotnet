using System;
using System.Collections.Generic;
using System.Data.Entity;//Necesario pata DbContext y DbSet
using System.Linq;
using System.Web;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Models
{
    public class AgendaPersonalCF_Hiriart_Corales : DbContext
    {
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }
        public DbSet<Memo> Memo { get; set; }
        public DbSet<Diario> Diario { get; set; }
        public DbSet<Contacto> Contacto { get; set; }

        public System.Data.Entity.DbSet<Hiriart_Corales_MVCWebApp_AgendaPersonal.Models.ListaEvento> ListaEventoes { get; set; }

        public System.Data.Entity.DbSet<Hiriart_Corales_MVCWebApp_AgendaPersonal.Models.ListaContacto> ListaContactoes { get; set; }
    }
}