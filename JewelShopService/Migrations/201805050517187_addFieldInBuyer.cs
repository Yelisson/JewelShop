namespace JewelShopService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldInBuyer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buyers", "mail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Buyers", "mail");
        }
    }
}
