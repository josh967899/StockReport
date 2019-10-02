namespace StockReport.Migrations
{
    using StockReport.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StockReport.Models.ApplicationDbContext>
    {
        
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
 
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "StockReport.Models.ApplicationDbContext";
            
        }
        
        protected override void Seed(StockReport.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Rates.AddOrUpdate(a => a.RateCode, new Rate
            {
                RateCode=1,Name="Fee",Value=0.001425M
            }, new Rate
            {
                RateCode = 2,
                Name = "Tax",
                Value = 0.003M
            });

        }
    }
}
