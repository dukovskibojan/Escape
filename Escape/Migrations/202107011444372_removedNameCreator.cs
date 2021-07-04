namespace Escape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedNameCreator : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Creators", "name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Creators", "name", c => c.String());
        }
    }
}
