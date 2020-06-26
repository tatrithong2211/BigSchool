namespace BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        FolloweeId = c.String(nullable: false, maxLength: 128),
                        CourseViewModel_Id = c.Int(),
                        CourseViewModel_Id1 = c.Int(),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FolloweeId })
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
                .ForeignKey("dbo.AspNetUsers", t => t.FolloweeId)
                .ForeignKey("dbo.CourseViewModels", t => t.CourseViewModel_Id)
                .ForeignKey("dbo.CourseViewModels", t => t.CourseViewModel_Id1)
                .Index(t => t.FollowerId)
                .Index(t => t.FolloweeId)
                .Index(t => t.CourseViewModel_Id)
                .Index(t => t.CourseViewModel_Id1);
            
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
            
            AddColumn("dbo.Attendances", "CourseViewModel_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Attendances", "CourseViewModel_Id");
            AddForeignKey("dbo.Attendances", "CourseViewModel_Id", "dbo.CourseViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "CourseViewModel_Id1", "dbo.CourseViewModels");
            DropForeignKey("dbo.Followings", "CourseViewModel_Id", "dbo.CourseViewModels");
            DropForeignKey("dbo.Attendances", "CourseViewModel_Id", "dbo.CourseViewModels");
            DropForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "FollowerId", "dbo.AspNetUsers");
            DropIndex("dbo.Followings", new[] { "CourseViewModel_Id1" });
            DropIndex("dbo.Followings", new[] { "CourseViewModel_Id" });
            DropIndex("dbo.Followings", new[] { "FolloweeId" });
            DropIndex("dbo.Followings", new[] { "FollowerId" });
            DropIndex("dbo.Attendances", new[] { "CourseViewModel_Id" });
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Attendances", "CourseViewModel_Id");
            DropTable("dbo.CourseViewModels");
            DropTable("dbo.Followings");
        }
    }
}
