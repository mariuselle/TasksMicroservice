namespace Tasks.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToTaskTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "UserId");
        }
    }
}
