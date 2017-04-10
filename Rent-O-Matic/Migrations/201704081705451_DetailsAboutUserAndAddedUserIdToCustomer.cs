namespace Rent_O_Matic.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DetailsAboutUserAndAddedUserIdToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "UserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "Nationality", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "YearsOld", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "DrivingLiscense", c => c.String(nullable: false, maxLength: 100));
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DrivingLiscense");
            DropColumn("dbo.AspNetUsers", "YearsOld");
            DropColumn("dbo.AspNetUsers", "Nationality");
            DropColumn("dbo.Customers", "UserId");
        }
    }
}
