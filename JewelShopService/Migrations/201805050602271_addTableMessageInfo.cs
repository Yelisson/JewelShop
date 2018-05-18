namespace JewelShopService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableMessageInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.String(),
                        FromMailAddress = c.String(),
                        Subject = c.String(),
                        Body = c.String(),
                        DateDelivery = c.DateTime(nullable: false),
                        buyerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buyers", t => t.buyerId)
                .Index(t => t.buyerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageInfoes", "buyerId", "dbo.Buyers");
            DropIndex("dbo.MessageInfoes", new[] { "buyerId" });
            DropTable("dbo.MessageInfoes");
        }
    }
}
