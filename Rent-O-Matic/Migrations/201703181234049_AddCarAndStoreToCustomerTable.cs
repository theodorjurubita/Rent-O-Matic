namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarAndStoreToCustomerTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "StoreId", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "CarId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "StoreId");
            CreateIndex("dbo.Customers", "CarId");
            AddForeignKey("dbo.Customers", "CarId", "dbo.Cars", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Customers", "StoreId", "dbo.Stores", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Customers", "CarId", "dbo.Cars");
            DropIndex("dbo.Customers", new[] { "CarId" });
            DropIndex("dbo.Customers", new[] { "StoreId" });
            DropColumn("dbo.Customers", "CarId");
            DropColumn("dbo.Customers", "StoreId");
        }
    }
}
