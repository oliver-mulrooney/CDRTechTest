using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CDR.Data.Migrations
{
    /// <inheritdoc />
    public partial class V1_0_0_InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CDRs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CallerId = table.Column<string>(type: "longtext", nullable: false),
                    Recipient = table.Column<string>(type: "longtext", nullable: false),
                    CallDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reference = table.Column<string>(type: "longtext", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDRs", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CDRs");
        }
    }
}
