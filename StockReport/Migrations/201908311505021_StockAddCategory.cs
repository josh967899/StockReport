namespace StockReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockAddCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stocks", "Category");
        }
    }
}
