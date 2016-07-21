namespace Chirper.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedLikeCount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Chirps", "LikeCount");
            DropColumn("dbo.Comments", "LikeCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.Chirps", "LikeCount", c => c.Int(nullable: false));
        }
    }
}
