namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCarsTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Cars( Brand, Model, Year, Price) VALUES ('Mercedes', 'C-Klasse', 2016, 100)");
            Sql("INSERT INTO Cars( Brand, Model, Year, Price) VALUES ('Audi', 'A4', 2015, 80)");
            Sql("INSERT INTO Cars( Brand, Model, Year, Price) VALUES ('BMW', '320d', 2014, 85)");
        }
        
        public override void Down()
        {
        }
    }
}
