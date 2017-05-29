namespace bookreview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDisplayedName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DisplayedUserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DisplayedUserName");
        }
    }
}
