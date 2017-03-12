namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCustomersTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Customers( Name, DrivingLiscense, YearsOld, Nationality) VALUES ( 'Ion Popescu', 'ABC123', 24, 'Romanian')");
            Sql("INSERT INTO Customers( Name, DrivingLiscense, YearsOld, Nationality) VALUES ( 'John Smith', 'ABC124', 34, 'American')");
        }
        
        public override void Down()
        {
        }
    }
}
