namespace Rent_O_Matic.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddIncidentGravityTableAndForeignKeyToRentalsHistories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncidentGravities",
                c => new
                {
                    Id = c.Byte(nullable: false),
                    Gravity = c.String(),
                    CoeficientToIncreasePrice = c.Single(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.RentalsHistories", "IncidentGravityId", c => c.Byte(nullable: true));
            CreateIndex("dbo.RentalsHistories", "IncidentGravityId");
            AddForeignKey("dbo.RentalsHistories", "IncidentGravityId", "dbo.IncidentGravities", "Id", cascadeDelete: false);
        }

        public override void Down()
        {
            DropForeignKey("dbo.RentalsHistories", "IncidentGravityId", "dbo.IncidentGravities");
            DropIndex("dbo.RentalsHistories", new[] { "IncidentGravityId" });
            DropColumn("dbo.RentalsHistories", "IncidentGravityId");
            DropTable("dbo.IncidentGravities");
        }
    }
}
