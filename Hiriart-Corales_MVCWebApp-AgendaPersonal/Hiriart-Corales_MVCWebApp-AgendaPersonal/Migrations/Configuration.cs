namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Hiriart_Corales_MVCWebApp_AgendaPersonal.Models.AgendaPersonalCF_Hiriart_Corales>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Hiriart_Corales_MVCWebApp_AgendaPersonal.Models.AgendaPersonalCF_Hiriart_Corales context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
