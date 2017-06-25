namespace Rent_O_Matic.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddCarPhotoToCarTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "CarPhoto", c => c.Binary());
        }

        public override void Down()
        {
            DropColumn("dbo.Cars", "CarPhoto");
        }
    }
}
