namespace Escape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredAdd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Arts", "artName", c => c.String(nullable: false));
            AlterColumn("dbo.Arts", "artUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Arts", "desc", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Arts", "desc", c => c.String());
            AlterColumn("dbo.Arts", "artUrl", c => c.String());
            AlterColumn("dbo.Arts", "artName", c => c.String());
        }
    }
}
