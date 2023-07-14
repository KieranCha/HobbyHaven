using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHaven.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class HobbyPersonalityTagRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HobbyPersonalityTag",
                columns: table => new
                {
                    HobbiesHobbyID = table.Column<Guid>(type: "TEXT", nullable: false),
                    PersonalityTagsPersonalityTagID = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbyPersonalityTag", x => new { x.HobbiesHobbyID, x.PersonalityTagsPersonalityTagID });
                    table.ForeignKey(
                        name: "FK_HobbyPersonalityTag_Hobbies_HobbiesHobbyID",
                        column: x => x.HobbiesHobbyID,
                        principalTable: "Hobbies",
                        principalColumn: "HobbyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HobbyPersonalityTag_PersonalityTags_PersonalityTagsPersonalityTagID",
                        column: x => x.PersonalityTagsPersonalityTagID,
                        principalTable: "PersonalityTags",
                        principalColumn: "PersonalityTagID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HobbyPersonalityTag_PersonalityTagsPersonalityTagID",
                table: "HobbyPersonalityTag",
                column: "PersonalityTagsPersonalityTagID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HobbyPersonalityTag");
        }
    }
}
