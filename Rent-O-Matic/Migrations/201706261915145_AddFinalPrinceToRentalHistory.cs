namespace Rent_O_Matic.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddFinalPrinceToRentalHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentalsHistories", "FinalPrice", c => c.Single(nullable: true));
        }

        public override void Down()
        {
            DropColumn("dbo.RentalsHistories", "FinalPrice");
        }
    }
}
