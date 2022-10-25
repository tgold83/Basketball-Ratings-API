using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasketballRatings.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    LastName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Team = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PositionName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerPosition",
                columns: table => new
                {
                    PlayerPositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPosition", x => x.PlayerPositionId);
                    table.ForeignKey(
                        name: "FK_PlayerPosition_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPosition_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "FirstName", "LastName", "Team" },
                values: new object[,]
                {
                    { 1, "LeBron", "James", "LAL" },
                    { 2, "Giannis", "Antetokoumpo", "MIL" },
                    { 3, "Stephen", "Curry", "GSW" },
                    { 4, "Kevin", "Durant", "BKN" },
                    { 5, "Nikola", "Jokic", "DEN" },
                    { 6, "Joel", "Embiid", "PHI" },
                    { 7, "Luka", "Doncic", "DAL" },
                    { 8, "Kawhi", "Leonard", "LAC" },
                    { 9, "Ja", "Morant", "MEM" },
                    { 10, "Jason", "Tatum", "BOS" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "PositionId", "PositionName" },
                values: new object[,]
                {
                    { 1, "PG" },
                    { 2, "SG" },
                    { 3, "SF" },
                    { 4, "PF" },
                    { 5, "C" }
                });

            migrationBuilder.InsertData(
                table: "PlayerPosition",
                columns: new[] { "PlayerPositionId", "PlayerId", "PositionId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 4, 2, 5 },
                    { 17, 10, 4 },
                    { 14, 8, 4 },
                    { 8, 4, 4 },
                    { 3, 2, 4 },
                    { 16, 10, 3 },
                    { 9, 5, 5 },
                    { 13, 8, 3 },
                    { 7, 4, 3 },
                    { 2, 1, 3 },
                    { 6, 3, 2 },
                    { 15, 9, 1 },
                    { 11, 7, 1 },
                    { 5, 3, 1 },
                    { 12, 7, 3 },
                    { 10, 6, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPosition_PlayerId",
                table: "PlayerPosition",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPosition_PositionId",
                table: "PlayerPosition",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerPosition");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
