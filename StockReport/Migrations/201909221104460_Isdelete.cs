namespace StockReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Isdelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dividends", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stocks", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Trades", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trades", "IsDelete");
            DropColumn("dbo.Stocks", "IsDelete");
            DropColumn("dbo.Dividends", "IsDelete");
        }
    }
}
