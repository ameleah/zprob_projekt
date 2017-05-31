namespace bookreview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "CreatedAt");
            DropColumn("dbo.Categories", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "UpdatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
