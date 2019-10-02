namespace StockReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDividend : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Dividends", "StockId");
            AddForeignKey("dbo.Dividends", "StockId", "dbo.Stocks", "Id", cascadeDelete: true);
            DropColumn("dbo.Dividends", "StockCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dividends", "StockCode", c => c.Int(nullable: false));
            DropForeignKey("dbo.Dividends", "StockId", "dbo.Stocks");
            DropIndex("dbo.Dividends", new[] { "StockId" });
        }
    }
}
