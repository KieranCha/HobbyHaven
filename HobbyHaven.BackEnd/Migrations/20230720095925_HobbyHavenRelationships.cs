using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHaven.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class HobbyHavenRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HavenHobby",
                columns: table => new
                {
                    HavensHavenID = table.Column<Guid>(type: "TEXT", nullable: false),
                    HobbiesHobbyID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HavenHobby", x => new { x.HavensHavenID, x.HobbiesHobbyID });
                    table.ForeignKey(
                        name: "FK_HavenHobby_Havens_HavensHavenID",
                        column: x => x.HavensHavenID,
                        principalTable: "Havens",
                        principalColumn: "HavenID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HavenHobby_Hobbies_HobbiesHobbyID",
                        column: x => x.HobbiesHobbyID,
                        principalTable: "Hobbies",
                        principalColumn: "HobbyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HavenHobby_HobbiesHobbyID",
                table: "HavenHobby",
                column: "HobbiesHobbyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HavenHobby");
        }
    }
}
