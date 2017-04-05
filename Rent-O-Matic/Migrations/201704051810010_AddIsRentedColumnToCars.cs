namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsRentedColumnToCars : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "IsRented", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "IsRented");
        }
    }
}
