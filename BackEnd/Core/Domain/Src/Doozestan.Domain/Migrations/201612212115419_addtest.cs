namespace Doozestan.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "test", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "test");
        }
    }
}
