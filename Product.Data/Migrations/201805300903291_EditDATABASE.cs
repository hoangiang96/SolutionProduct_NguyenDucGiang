namespace Product.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditDATABASE : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Products", newName: "Items");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Items", newName: "Products");
        }
    }
}
