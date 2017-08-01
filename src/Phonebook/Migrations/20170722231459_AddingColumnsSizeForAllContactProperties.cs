using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Phonebook.Migrations
{
    public partial class AddingColumnsSizeForAllContactProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "Tags",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Phones",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Emails",
                maxLength: 254,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Contacts",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactName",
                table: "Contacts",
                maxLength: 100,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Contacts",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Contacts",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalInfo",
                table: "Contacts",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "Tags",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Phones",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Emails",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ZipCode",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactName",
                table: "Contacts",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Contacts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalInfo",
                table: "Contacts",
                nullable: true);
        }
    }
}
