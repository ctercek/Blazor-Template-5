using Microsoft.EntityFrameworkCore.Migrations;

namespace btp.Data.Migrations
{
    using System;

    public partial class securitymodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateTable(
               name: "AspNetAddresses",
               columns: table => new
               {
                   AddressId = table.Column<string>(nullable: false, defaultValueSql: "NEWID()"),
                   UserId = table.Column<string>(maxLength: 450, nullable: false),
                   Default = table.Column<bool>(nullable: false, defaultValue: false),
                   Name = table.Column<string>(maxLength: 256, nullable: false),
                   AddressOne = table.Column<string>(maxLength: 256, nullable: false),
                   AddressTwo = table.Column<string>(maxLength: 256, nullable: true),
                   AddressThree = table.Column<string>(maxLength: 256, nullable: true),
                   City = table.Column<string>(maxLength: 256, nullable: false),
                   State = table.Column<string>(maxLength: 256, nullable: false),
                   ZipCode = table.Column<string>(maxLength: 256, nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_AspNetAddresses", x => new { x.UserId, x.Name });
                   table.ForeignKey(
                       name: "FK_AspNetUserTokens_AspNetAddresses_UserId",
                       column: x => x.UserId,
                       principalTable: "AspNetUsers",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateTable(
                name: "AspNetPhones",
                columns: table => new
                {
                    PhoneId = table.Column<string>(nullable: false, defaultValueSql: "NEWID()"),
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetPhones", x => new { x.UserId, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetPhones_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //Create Users and roles if necessary
            //var defaultIdentity = new DefaultIdentity(migrationBuilder);
            //defaultIdentity.CreateUsers();
            //defaultIdentity.CreateRoles();
            //defaultIdentity.CreateUserToRoles();
            //defaultIdentity.CreateAddress();
            //defaultIdentity.CreatePhone();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetAddresses");

            migrationBuilder.DropTable(
                name: "AspNetPhones");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
