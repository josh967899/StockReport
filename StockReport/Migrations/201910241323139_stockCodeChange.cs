namespace StockReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class stockCodeChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "StockCodeTmp", c => c.String(nullable: false));
            Sql(@"
    UPDATE dbo.Stocks
    SET StockCodeTmp =StockCode     
    ");
            DropColumn("dbo.Stocks", "StockCode");
            RenameColumn("dbo.Stocks", "StockCodeTmp", "StockCode");
        }

        public override void Down()
        {
        }
    }
}
