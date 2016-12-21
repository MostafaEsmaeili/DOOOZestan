namespace Doozestan.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeuser : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Game", "WinnerId");
            CreateIndex("dbo.Tournament", "UserId");
            AddForeignKey("dbo.Tournament", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Game", "WinnerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Game", "WinnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tournament", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Tournament", new[] { "UserId" });
            DropIndex("dbo.Game", new[] { "WinnerId" });
        }
    }
}
