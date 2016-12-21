namespace Doozestan.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeTest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "test", c => c.Boolean());
        }
    }
}
