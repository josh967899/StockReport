namespace StockReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTradeandStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "CurrentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Trades", "UserCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trades", "UserCode");
            DropColumn("dbo.Stocks", "CurrentPrice");
        }
    }
}
