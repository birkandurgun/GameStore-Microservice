using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PublisherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => new { x.GameId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_GameGenre_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0535780e-307b-4046-9b59-e937b6a8c5c1"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simulation", null },
                    { new Guid("7177b6e3-582d-47ad-8cc0-1809b9410db0"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action", null },
                    { new Guid("a3a1e6cf-325f-4dbd-b3b0-b45f4a77e416"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indie", null },
                    { new Guid("a51661ff-6012-42db-9cf9-5c68cfa7f038"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Racing", null },
                    { new Guid("a83ff907-1c4b-45ed-90bb-b1332c620e5f"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Early Access", null },
                    { new Guid("b9fda6d2-8d9d-4a5e-975d-78b8ef3c5667"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Casual", null },
                    { new Guid("c5c8f2a3-2c68-47f2-9507-9f90639a1d8b"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Massively Multiplayer", null },
                    { new Guid("cf3d6d98-3b93-4d7f-aed6-e24e1d3ecf33"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Strategy", null },
                    { new Guid("d17c7345-36d2-48c7-8000-1be1231e84e5"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Free To Play", null },
                    { new Guid("ed8bc02f-9cde-46b7-b035-cbd58f788fc4"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "RPG", null },
                    { new Guid("ee983b67-56ad-4f92-8125-9c1c740e76b9"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adventure", null },
                    { new Guid("f51a7796-7a07-47cc-8c99-e105f1d7c0e0"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sports", null }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("22ed2c3e-7b7b-4f67-8a4f-4c79b7cf3209"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ubisoft", null },
                    { new Guid("3f6e8791-10a0-4317-9879-79a1d3b18f0e"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "CD Projekt", null },
                    { new Guid("595167fa-d015-44f3-8a27-0db55cb2efaf"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Square Enix", null },
                    { new Guid("7623a287-6012-429b-b53a-243da6e5f3b2"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paradox Interactive", null },
                    { new Guid("a0914389-bd9a-4737-a45d-b54b378b6d56"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bethesda Softworks", null },
                    { new Guid("c68fe36e-746b-4d82-a9fc-e9fd16d2f6f2"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Valve", null },
                    { new Guid("c7a86de5-b230-41e2-b0fc-bb75be1301bc"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Take-Two Interactive", null },
                    { new Guid("cf8f595f-bb19-4c0d-b5b4-bfc859f580fb"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electronic Arts", null },
                    { new Guid("dbf5bfa1-3d7d-47e4-883f-42b1c3184e92"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Activision", null },
                    { new Guid("e0c0f222-d9de-4ae7-bf0b-5d280473b8b4"), new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bandai Namco Entertainment", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenreId",
                table: "GameGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
