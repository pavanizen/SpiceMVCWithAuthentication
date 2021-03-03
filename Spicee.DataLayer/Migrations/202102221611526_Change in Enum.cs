namespace Spicee.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeinEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Coupons", "CouponType", c => c.Int(nullable: false));
            AlterColumn("dbo.MenuItems", "Spicyness", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MenuItems", "Spicyness", c => c.String());
            AlterColumn("dbo.Coupons", "CouponType", c => c.String(nullable: false));
        }
    }
}
