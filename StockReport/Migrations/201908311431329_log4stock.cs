namespace StockReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class log4stock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogStockUpdates",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UpdateDate = c.DateTime(nullable: false),
                        Log = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogStockUpdates");
        }
    }
}
