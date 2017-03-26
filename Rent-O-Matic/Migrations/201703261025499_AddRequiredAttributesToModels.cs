namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredAttributesToModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "Brand", c => c.String(nullable: false));
            AlterColumn("dbo.Cars", "Model", c => c.String(nullable: false));
            AlterColumn("dbo.Stores", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Stores", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "DrivingLiscense", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Nationality", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Nationality", c => c.String());
            AlterColumn("dbo.Customers", "DrivingLiscense", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            AlterColumn("dbo.Stores", "Country", c => c.String());
            AlterColumn("dbo.Stores", "City", c => c.String());
            AlterColumn("dbo.Cars", "Model", c => c.String());
            AlterColumn("dbo.Cars", "Brand", c => c.String());
        }
    }
}
