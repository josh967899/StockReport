namespace StockReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trades : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TradeDate = c.DateTime(nullable: false),
                        StockId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TradeType = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fee = c.Int(nullable: false),
                        Tax = c.Int(nullable: false),
                        TotalAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trades", "StockId", "dbo.Stocks");
            DropIndex("dbo.Trades", new[] { "StockId" });
            DropTable("dbo.Trades");
        }
    }
}
