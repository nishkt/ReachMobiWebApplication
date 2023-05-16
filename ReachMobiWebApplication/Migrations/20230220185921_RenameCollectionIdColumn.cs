using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReachMobiWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class RenameCollectionIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "collectionId",
                table: "ClickData",
                newName: "trackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "trackId",
                table: "ClickData",
                newName: "collectionId");
        }
    }
}
