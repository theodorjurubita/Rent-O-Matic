namespace Rent_O_Matic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeyStoreForCarTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "StoreId");
            AddForeignKey("dbo.Cars", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "StoreId", "dbo.Stores");
            DropIndex("dbo.Cars", new[] { "StoreId" });
            DropColumn("dbo.Cars", "StoreId");
        }
    }
}
