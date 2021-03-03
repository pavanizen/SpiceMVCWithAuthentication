namespace SpiceMVCWithAuthentication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CitycolAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "city", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "city");
        }
    }
}
