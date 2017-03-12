namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStoresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Stores(City, Country) VALUES ('Brasov', 'Romania')");
            Sql("INSERT INTO Stores(City, Country) VALUES ('Arad', 'Romania')");
            Sql("INSERT INTO Stores(City, Country) VALUES ('Budapesta', 'Ungaria')");
            Sql("INSERT INTO Stores(City, Country) VALUES ('Viena', 'Austria')");
            Sql("INSERT INTO Stores(City, Country) VALUES ('Bucuresti', 'Romania')");
            Sql("INSERT INTO Stores(City, Country) VALUES ('Pitesti', 'Romania')");
            Sql("INSERT INTO Stores(City, Country) VALUES ('Sofia', 'Bulgaria')");
        }
        
        public override void Down()
        {
        }
    }
}
