namespace Escape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userNameAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creators", "username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Creators", "username");
        }
    }
}
