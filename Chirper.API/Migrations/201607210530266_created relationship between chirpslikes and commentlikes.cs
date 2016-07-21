namespace Chirper.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdrelationshipbetweenchirpslikesandcommentlikes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Chirps", "UserId", "dbo.AspNetUsers");
            CreateTable(
                "dbo.CommentLikes",
                c => new
                    {
                        Comment_CommentId = c.Int(nullable: false),
                        ChirperUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Comment_CommentId, t.ChirperUser_Id })
                .ForeignKey("dbo.Comments", t => t.Comment_CommentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ChirperUser_Id, cascadeDelete: true)
                .Index(t => t.Comment_CommentId)
                .Index(t => t.ChirperUser_Id);
            
            CreateTable(
                "dbo.ChirpLikes",
                c => new
                    {
                        Chirp_ChirpId = c.Int(nullable: false),
                        ChirperUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Chirp_ChirpId, t.ChirperUser_Id })
                .ForeignKey("dbo.Chirps", t => t.Chirp_ChirpId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ChirperUser_Id, cascadeDelete: true)
                .Index(t => t.Chirp_ChirpId)
                .Index(t => t.ChirperUser_Id);
            
            AddForeignKey("dbo.Chirps", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Chirps", "LikedUsersAsString");
            DropColumn("dbo.Comments", "LikedUsersAsString");
            DropColumn("dbo.AspNetUsers", "LikedCommentsAsString");
            DropColumn("dbo.AspNetUsers", "LikedChirpsAsString");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "LikedChirpsAsString", c => c.String());
            AddColumn("dbo.AspNetUsers", "LikedCommentsAsString", c => c.String());
            AddColumn("dbo.Comments", "LikedUsersAsString", c => c.String());
            AddColumn("dbo.Chirps", "LikedUsersAsString", c => c.String());
            DropForeignKey("dbo.Chirps", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChirpLikes", "ChirperUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChirpLikes", "Chirp_ChirpId", "dbo.Chirps");
            DropForeignKey("dbo.CommentLikes", "ChirperUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentLikes", "Comment_CommentId", "dbo.Comments");
            DropIndex("dbo.ChirpLikes", new[] { "ChirperUser_Id" });
            DropIndex("dbo.ChirpLikes", new[] { "Chirp_ChirpId" });
            DropIndex("dbo.CommentLikes", new[] { "ChirperUser_Id" });
            DropIndex("dbo.CommentLikes", new[] { "Comment_CommentId" });
            DropTable("dbo.ChirpLikes");
            DropTable("dbo.CommentLikes");
            AddForeignKey("dbo.Chirps", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
