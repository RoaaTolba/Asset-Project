using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetsPro.Migrations
{
    /// <inheritdoc />
    public partial class beforeGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_Group_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Group_id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Group_id",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Group_id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Group_id",
                table: "Users",
                column: "Group_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_Group_id",
                table: "Users",
                column: "Group_id",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
