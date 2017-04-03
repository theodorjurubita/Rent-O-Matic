namespace Rent_O_Matic.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name]) VALUES (N'1c54f388-7202-40df-aff5-27f92ce8f8d6', N'admin@rent.com', 0, N'AM9yHNj6MUFB8qDXo42TEDGikO4PQ6sMQDPWFF70jBdd4Di92EWNVSt5MLyI5ItMAg==', N'65eca1c6-23da-44fc-8eca-4ef2e16b90dc', NULL, 0, 0, NULL, 1, 0, N'admin@rent.com', N'Car Adminus')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name]) VALUES (N'a383b99f-811a-41c6-9f5c-93628cf6f97c', N'farfuridi@rent.com', 0, N'AF+9Lnekbl0ov9OGf6S4L2lmflwTkK5EhnJNirFUfg2cLNqaUwmmacnLLSqONs/RZw==', N'504a717f-808e-4c57-82cd-0300f7d0eaca', NULL, 0, 0, NULL, 1, 0, N'farfuridi@rent.com', N'Farfuridi Branzovenescu')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f6806c0a-1537-46b6-87f5-0837e26132b8', N'CanManageCars')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1c54f388-7202-40df-aff5-27f92ce8f8d6', N'f6806c0a-1537-46b6-87f5-0837e26132b8')

");
        }

        public override void Down()
        {
        }
    }
}
