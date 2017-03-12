namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Stores( City, Country) VALUES ('Bucharest', 'Romania')");
            Sql("INSERT INTO Stores( City, Country) VALUES ('Pitesti', 'Romania')");
            Sql("INSERT INTO Stores( City, Country) VALUES ('Iasi', 'Romania')");

        }
        
        public override void Down()
        {
        }
    }
}
