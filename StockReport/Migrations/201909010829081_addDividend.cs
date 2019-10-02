namespace StockReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDividend : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dividends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        StockId = c.Int(nullable: false),
                        StockCode = c.Int(nullable: false),
                        DividendTyoe = c.String(),
                        Among = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dividends");
        }
    }
}
