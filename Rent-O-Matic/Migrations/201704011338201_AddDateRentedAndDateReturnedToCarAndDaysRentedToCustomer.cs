namespace Rent_O_Matic.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDateRentedAndDateReturnedToCarAndDaysRentedToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "LastDateRented", c => c.DateTime(nullable: true));
            AddColumn("dbo.Cars", "LastDateReturned", c => c.DateTime(nullable: true));
            AddColumn("dbo.Customers", "DaysRented", c => c.Int(nullable: true));
        }

        public override void Down()
        {
            DropColumn("dbo.Customers", "DaysRented");
            DropColumn("dbo.Cars", "LastDateReturned");
            DropColumn("dbo.Cars", "LastDateRented");
        }
    }
}
