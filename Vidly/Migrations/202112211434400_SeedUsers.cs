namespace Vidly.Migrations {
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration {
        public override void Up() {
			Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1fa98d02-9e1f-4e7d-9f47-43e7e7b31109', N'admin@vidly.com', 0, N'AGOE7yzN8Tqb8PzypiWm8qHqL8krDmBuNvwcdouUBSiNY5QdrhqBgA/HUIfACf8iuA==', N'b31bd763-ac44-4bca-8788-06bc2b529691', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b85c202d-ed22-44c2-a744-bbe91f838a47', N'guest@vidly.com', 0, N'AL4f1EiHiXP0XTw28E/bOEAFfgiGfkATn7tM1r4WhXCNTdc9ozR3k+uUjMohgBIpVw==', N'3c9107dc-1929-4e37-97c9-dc4269a5e4af', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7c1ee9a0-cc7f-4ce7-b61f-a59378130670', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1fa98d02-9e1f-4e7d-9f47-43e7e7b31109', N'7c1ee9a0-cc7f-4ce7-b61f-a59378130670')
");
        }
        
        public override void Down() {
        }
    }
}