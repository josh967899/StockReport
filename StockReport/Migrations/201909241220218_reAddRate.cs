namespace StockReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reAddRate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RateCode = c.Int(nullable: false),
                        Name = c.String(),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rates");
        }
    }
}
