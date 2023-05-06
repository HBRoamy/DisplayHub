using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDomain.Migrations
{
    public partial class likesDislikesCountersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "DisplayableItems");

            migrationBuilder.AddColumn<int>(
                name: "LikesCounter",
                table: "DisplayableItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "dislikesCounter",
                table: "DisplayableItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikesCounter",
                table: "DisplayableItems");

            migrationBuilder.DropColumn(
                name: "dislikesCounter",
                table: "DisplayableItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "DisplayableItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
