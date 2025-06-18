using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Threads.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserProfileIdToUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClerkUserId",
                table: "Users",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "ClerkUserId");
        }
    }
}
