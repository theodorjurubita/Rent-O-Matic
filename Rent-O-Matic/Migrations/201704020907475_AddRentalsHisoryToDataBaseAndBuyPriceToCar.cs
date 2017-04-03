namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRentalsHisoryToDataBaseAndBuyPriceToCar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RentalsHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        DateRented = c.DateTime(nullable: false),
                        DateReturned = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Cars", "AcquisitionPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RentalsHistories", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.RentalsHistories", "CarId", "dbo.Cars");
            DropIndex("dbo.RentalsHistories", new[] { "CustomerId" });
            DropIndex("dbo.RentalsHistories", new[] { "CarId" });
            DropColumn("dbo.Cars", "AcquisitionPrice");
            DropTable("dbo.RentalsHistories");
        }
    }
}
