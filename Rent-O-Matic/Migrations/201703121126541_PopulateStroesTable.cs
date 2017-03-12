namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateStroesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Stores(City, Country) VALUES ('Brasov', 'Romania')");
            Sql("INSERT INTO Stores(City, Country) VALUES ('Arad', 'Romania')");
            Sql("INSERT INTO Stores(City, Country) VALUES ('Budapesta', 'Ungaria')");
            Sql("INSERT INTO Stores(City, Country) VALUES ('Viena', 'Austria')");
        }
        
        public override void Down()
        {
        }
    }
}
