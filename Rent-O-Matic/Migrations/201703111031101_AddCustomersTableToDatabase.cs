namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomersTableToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DrivingLiscense = c.String(),
                        YearsOld = c.Int(nullable: false),
                        Nationality = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
