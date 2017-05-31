namespace bookreview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.Reviews", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Reviews", new[] { "User_Id" });
            DropIndex("dbo.Reviews", new[] { "Book_Id" });
            DropIndex("dbo.Reviews", new[] { "Author_Id" });
            DropTable("dbo.Reviews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityType = c.Boolean(nullable: false),
                        Text = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Book_Id = c.Int(),
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Reviews", "Author_Id");
            CreateIndex("dbo.Reviews", "Book_Id");
            CreateIndex("dbo.Reviews", "User_Id");
            AddForeignKey("dbo.Reviews", "Author_Id", "dbo.Authors", "Id");
            AddForeignKey("dbo.Reviews", "Book_Id", "dbo.Books", "Id");
            AddForeignKey("dbo.Reviews", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
