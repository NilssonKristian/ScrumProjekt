namespace ScrumProjekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        TimeSent = c.DateTime(nullable: false),
                        SenderId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId_Id)
                .Index(t => t.SenderId_Id);
            
            DropTable("dbo.InläggModels");
        }
        
        public override void Down()
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
            
            DropForeignKey("dbo.PostModels", "SenderId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PostModels", new[] { "SenderId_Id" });
            DropTable("dbo.PostModels");
        }
    }
}
