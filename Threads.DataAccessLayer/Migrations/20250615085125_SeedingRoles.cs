using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Threads.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] 
                {Guid.NewGuid(), Statics.User_Role, Statics.User_Role.ToUpper(), Guid.NewGuid().ToString()}
            );

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] 
                { Guid.NewGuid(), Statics.Admin_Role, Statics.Admin_Role.ToUpper(), Guid.NewGuid().ToString() }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From AspNetRoles");
        }
    }
}
