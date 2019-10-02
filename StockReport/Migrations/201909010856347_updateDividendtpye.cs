namespace StockReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDividendtpye : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dividends", "DividendType", c => c.String());
            DropColumn("dbo.Dividends", "DividendTyoe");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dividends", "DividendTyoe", c => c.String());
            DropColumn("dbo.Dividends", "DividendType");
        }
    }
}
