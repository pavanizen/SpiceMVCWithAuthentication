namespace Spicee.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changingfieldofenum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MenuItems", "Spicyness", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MenuItems", "Spicyness", c => c.Int(nullable: false));
        }
    }
}
