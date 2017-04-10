namespace Rent_O_Matic.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CarAndStoreNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Customers", "StoreId", "dbo.Stores");
            DropIndex("dbo.Customers", new[] { "StoreId" });
            DropIndex("dbo.Customers", new[] { "CarId" });
            AlterColumn("dbo.Customers", "StoreId", c => c.Int());
            AlterColumn("dbo.Customers", "CarId", c => c.Int());
            CreateIndex("dbo.Customers", "StoreId");
            CreateIndex("dbo.Customers", "CarId");
            AddForeignKey("dbo.Customers", "CarId", "dbo.Cars", "Id");
            AddForeignKey("dbo.Customers", "StoreId", "dbo.Stores", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Customers", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Customers", "CarId", "dbo.Cars");
            DropIndex("dbo.Customers", new[] { "CarId" });
            DropIndex("dbo.Customers", new[] { "StoreId" });
            AlterColumn("dbo.Customers", "CarId", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "CarId");
            CreateIndex("dbo.Customers", "StoreId");
            AddForeignKey("dbo.Customers", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Customers", "CarId", "dbo.Cars", "Id", cascadeDelete: true);
        }
    }
}
