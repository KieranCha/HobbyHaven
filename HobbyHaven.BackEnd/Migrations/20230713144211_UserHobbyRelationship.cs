using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHaven.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UserHobbyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HobbyUser",
                columns: table => new
                {
                    HobbiesHobbyID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersUserID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbyUser", x => new { x.HobbiesHobbyID, x.UsersUserID });
                    table.ForeignKey(
                        name: "FK_HobbyUser_Hobbies_HobbiesHobbyID",
                        column: x => x.HobbiesHobbyID,
                        principalTable: "Hobbies",
                        principalColumn: "HobbyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HobbyUser_Users_UsersUserID",
                        column: x => x.UsersUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HobbyUser_UsersUserID",
                table: "HobbyUser",
                column: "UsersUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HobbyUser");
        }
    }
}
