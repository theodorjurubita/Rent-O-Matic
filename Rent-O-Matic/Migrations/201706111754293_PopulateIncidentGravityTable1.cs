namespace Rent_O_Matic.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateIncidentGravityTable1 : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO[dbo].[IncidentGravities] ([Id], [Gravity], [CoeficientToIncreasePrice]) VALUES(5, N'NoIncident', 0.9)");
        }

        public override void Down()
        {
        }
    }
}
