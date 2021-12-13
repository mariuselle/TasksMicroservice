namespace Tasks.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypesOfColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "UserId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "UserId", c => c.Int(nullable: false));
        }
    }
}
