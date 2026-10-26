using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocketCardsUnlimited.Migrations
{
    /// <inheritdoc />
    public partial class AddSportsCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SportsCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Sport = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Team = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Manufacturer = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    CardNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Condition = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    EstimatedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsCards", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SportsCards");
        }
    }
}
