namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHasCarsColumnToStoreTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "HasCars", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stores", "HasCars");
        }
    }
}
