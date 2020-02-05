namespace SaemNaKniga.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prva : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Authors", "Gender", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Authors", "Gender", c => c.Int(nullable: false));
        }
    }
}
