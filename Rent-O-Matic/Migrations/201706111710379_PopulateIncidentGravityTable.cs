namespace Rent_O_Matic.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateIncidentGravityTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[IncidentGravities] ([Id], [Gravity], [CoeficientToIncreasePrice]) VALUES (1, N'Minor', 1.1)
INSERT INTO [dbo].[IncidentGravities] ([Id], [Gravity], [CoeficientToIncreasePrice]) VALUES (2, N'Medium', 1.3)
INSERT INTO [dbo].[IncidentGravities] ([Id], [Gravity], [CoeficientToIncreasePrice]) VALUES (3, N'High', 1.7)
INSERT INTO [dbo].[IncidentGravities] ([Id], [Gravity], [CoeficientToIncreasePrice]) VALUES (4, N'FullDamage', 2.2)


                ");
        }

        public override void Down()
        {
        }
    }
}
