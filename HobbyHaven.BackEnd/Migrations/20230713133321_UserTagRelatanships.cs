﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHaven.BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UserTagRelatanships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Havens",
                columns: table => new
                {
                    HavenID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Havens", x => x.HavenID);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    HobbyID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.HobbyID);
                });

            migrationBuilder.CreateTable(
                name: "PersonalityTags",
                columns: table => new
                {
                    PersonalityTagID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityTags", x => x.PersonalityTagID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "TEXT", nullable: false),
                    Admin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "PersonalityTagUser",
                columns: table => new
                {
                    PersonalityTagsPersonalityTagID = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersUserID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityTagUser", x => new { x.PersonalityTagsPersonalityTagID, x.UsersUserID });
                    table.ForeignKey(
                        name: "FK_PersonalityTagUser_PersonalityTags_PersonalityTagsPersonalityTagID",
                        column: x => x.PersonalityTagsPersonalityTagID,
                        principalTable: "PersonalityTags",
                        principalColumn: "PersonalityTagID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalityTagUser_Users_UsersUserID",
                        column: x => x.UsersUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityTagUser_UsersUserID",
                table: "PersonalityTagUser",
                column: "UsersUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Havens");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "PersonalityTagUser");

            migrationBuilder.DropTable(
                name: "PersonalityTags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}