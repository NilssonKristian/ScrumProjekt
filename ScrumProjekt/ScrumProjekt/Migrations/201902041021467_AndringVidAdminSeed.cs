namespace ScrumProjekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AndringVidAdminSeed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "isAdmin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "isAdmin", c => c.Boolean(nullable: false));
        }
    }
}
