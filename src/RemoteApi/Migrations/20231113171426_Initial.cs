using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemoteApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    ConcertType = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Conductor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocalistVoice = table.Column<int>(type: "int", nullable: true),
                    Headliner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HowToReach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinAge = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Sub = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Sub);
                });

            migrationBuilder.CreateTable(
                name: "ConcertInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Performer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Address_Country = table.Column<int>(type: "int", nullable: false),
                    Location_Address_Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Address_Building = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Address_Floor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Address_Room = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Address_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTickets = table.Column<int>(type: "int", nullable: false),
                    SoldTickets = table.Column<int>(type: "int", nullable: false),
                    ConcertId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConcertInfo_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserConcert",
                columns: table => new
                {
                    ConcertsId = table.Column<long>(type: "bigint", nullable: false),
                    UsersSub = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserConcert", x => new { x.ConcertsId, x.UsersSub });
                    table.ForeignKey(
                        name: "FK_AppUserConcert_Concerts_ConcertsId",
                        column: x => x.ConcertsId,
                        principalTable: "Concerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserConcert_Users_UsersSub",
                        column: x => x.UsersSub,
                        principalTable: "Users",
                        principalColumn: "Sub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserConcert_UsersSub",
                table: "AppUserConcert",
                column: "UsersSub");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertInfo_ConcertId",
                table: "ConcertInfo",
                column: "ConcertId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserConcert");

            migrationBuilder.DropTable(
                name: "ConcertInfo");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Concerts");
        }
    }
}
