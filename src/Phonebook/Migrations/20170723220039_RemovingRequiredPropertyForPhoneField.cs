using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Phonebook.Migrations
{
    public partial class RemovingRequiredPropertyForPhoneField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Phones",
                maxLength: 50,
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
