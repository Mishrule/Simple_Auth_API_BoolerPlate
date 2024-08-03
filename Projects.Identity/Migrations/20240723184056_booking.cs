using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projects.Identity.Migrations
{
    /// <inheritdoc />
    public partial class booking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildingType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonResidentialBooking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptanceState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonResidentialBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NonResidentialBooking_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResidentialBooking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptanceState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResidentialBooking_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "68f18aba-2ea0-4757-86cc-ac7c4bbf570b", "AQAAAAIAAYagAAAAEIbjBvAXal70sy4t+Ia22Zw9N9qYhM7Ncg+Sq+xn9nM+A3AJqIWB05Z7RJEEKoRkdw==", "f7180060-e896-4111-8959-3f1b29f18634" });

            migrationBuilder.CreateIndex(
                name: "IX_NonResidentialBooking_BuildingId",
                table: "NonResidentialBooking",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialBooking_BuildingId",
                table: "ResidentialBooking",
                column: "BuildingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NonResidentialBooking");

            migrationBuilder.DropTable(
                name: "ResidentialBooking");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9232edd1-5818-48ce-bad8-665dafa484cd", "AQAAAAIAAYagAAAAEOmcC40uvhSRfjN/ZWMGAEdB1IgdcFGtftosX13HP5uLlUgWq4GJUiwRCbsa0dDAzg==", "6bef4ab2-9143-473f-adff-59afe6da88ab" });
        }
    }
}
