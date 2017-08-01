using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Phonebook.Migrations
{
    public partial class RemovingFaxPropertyFromContactEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactTag_ContactId",
                table: "ContactTag");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Contacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Contacts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactTag_ContactId",
                table: "ContactTag",
                column: "ContactId");
        }
    }
}
