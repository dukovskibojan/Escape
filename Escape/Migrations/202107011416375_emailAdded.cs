namespace Escape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Creators", "email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Creators", "email");
        }
    }
}
