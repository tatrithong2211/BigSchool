namespace BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendances", "CourseViewModel_Id", "dbo.CourseViewModels");
            DropForeignKey("dbo.Followings", "CourseViewModel_Id", "dbo.CourseViewModels");
            DropForeignKey("dbo.Followings", "CourseViewModel_Id1", "dbo.CourseViewModels");
            DropIndex("dbo.Attendances", new[] { "CourseViewModel_Id" });
            DropIndex("dbo.Followings", new[] { "CourseViewModel_Id" });
            DropIndex("dbo.Followings", new[] { "CourseViewModel_Id1" });
            AddColumn("dbo.Courses", "IsCanceled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Attendances", "CourseViewModel_Id");
            DropColumn("dbo.Followings", "CourseViewModel_Id");
            DropColumn("dbo.Followings", "CourseViewModel_Id1");
            DropTable("dbo.CourseViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourseViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Place = c.String(nullable: false),
                        Date = c.String(nullable: false),
                        Time = c.String(nullable: false),
                        Category = c.Byte(nullable: false),
                        Heading = c.String(),
                        searching = c.String(),
                        ShowAction = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Followings", "CourseViewModel_Id1", c => c.Int());
            AddColumn("dbo.Followings", "CourseViewModel_Id", c => c.Int());
            AddColumn("dbo.Attendances", "CourseViewModel_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Courses", "IsCanceled");
            CreateIndex("dbo.Followings", "CourseViewModel_Id1");
            CreateIndex("dbo.Followings", "CourseViewModel_Id");
            CreateIndex("dbo.Attendances", "CourseViewModel_Id");
            AddForeignKey("dbo.Followings", "CourseViewModel_Id1", "dbo.CourseViewModels", "Id");
            AddForeignKey("dbo.Followings", "CourseViewModel_Id", "dbo.CourseViewModels", "Id");
            AddForeignKey("dbo.Attendances", "CourseViewModel_Id", "dbo.CourseViewModels", "Id");
        }
    }
}
