namespace Vidly.Migrations {
    using System.Data.Entity.Migrations;
    
    public partial class AddGenre : DbMigration {
        public override void Up() {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);

			AddColumn("dbo.Movies", "GenreId", c => c.Byte(nullable: false));
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "Stock", c => c.Byte(nullable: false));
            CreateIndex("dbo.Movies", "GenreId");
            AddForeignKey("dbo.Movies", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);

			Sql("INSERT INTO Genres(Id, Name) VALUES(1, 'Comedy')");
			Sql("INSERT INTO Genres(Id, Name) VALUES(2, 'Action')");
			Sql("INSERT INTO Genres(Id, Name) VALUES(3, 'Family')");
			Sql("INSERT INTO Genres(Id, Name) VALUES(4, 'Drama')");
			Sql("INSERT INTO Genres(Id, Name) VALUES(5, 'Romance')");
		}
        
        public override void Down() {
            DropForeignKey("dbo.Movies", "GenreId", "dbo.Genres");
            DropIndex("dbo.Movies", new[] { "GenreId" });
            DropColumn("dbo.Movies", "Stock");
            DropColumn("dbo.Movies", "DateAdded");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropColumn("dbo.Movies", "GenreId");
            DropTable("dbo.Genres");
		}
    }
}