namespace Product.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intergrategiang : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUsers", "BirthDay", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUsers", "BirthDay", c => c.DateTime(nullable: false));
        }
    }
}
