namespace ScrumProjekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Skapat_inläggstabellen : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InläggModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        Content = c.String(),
                        TimeSent = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InläggModels");
        }
    }
}
