using Microsoft.EntityFrameworkCore.Migrations;

namespace btp.Data.Migrations
{
    public partial class textmessaging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UseForTextMessaging",
                table: "AspNetPhones",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseForTextMessaging",
                table: "AspNetPhones");
        }
    }
}
