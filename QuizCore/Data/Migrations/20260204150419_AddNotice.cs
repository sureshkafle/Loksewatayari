using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNotice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "users",
                newName: "last_name");

            migrationBuilder.AddColumn<int>(
                name: "active_status",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "date_of_birth",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "middle_name",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "notices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    file = table.Column<string[]>(type: "text[]", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    attachment_url = table.Column<string>(type: "text", nullable: true),
                    active_status = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expiry_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    publish_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notices", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notices");

            migrationBuilder.DropColumn(
                name: "active_status",
                table: "users");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "users");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "middle_name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "updated_date",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "users",
                newName: "full_name");
        }
    }
}
