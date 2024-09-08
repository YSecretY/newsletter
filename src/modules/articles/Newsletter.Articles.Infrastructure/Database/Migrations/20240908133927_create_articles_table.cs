using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Newsletter.Articles.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class create_articles_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "articles");

            migrationBuilder.CreateTable(
                name: "Articles",
                schema: "articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(2056)", maxLength: 2056, nullable: false),
                    Description = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    Content = table.Column<string>(type: "character varying(1000000)", maxLength: 1000000, nullable: false),
                    Tags = table.Column<string[]>(type: "character varying(512)[]", nullable: false),
                    Slug = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ViewsCount = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles",
                schema: "articles");
        }
    }
}
