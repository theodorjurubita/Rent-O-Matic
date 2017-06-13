namespace Rent_O_Matic.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AlterForeignKeyRentalsHistories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RentalsHistories", "IncidentGravityId", "dbo.IncidentGravities");
            Sql(@"ALTER TABLE dbo.RentalsHistories
                ADD FOREIGN KEY (IncidentGravityId) REFERENCES dbo.IncidentGravities(Id) on update cascade;");
        }

        public override void Down()
        {
        }
    }
}
