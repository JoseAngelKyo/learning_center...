using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Practica04.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "emergency_incidents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    guid_code = table.Column<Guid>(type: "char(36)", nullable: false),
                    response_unit_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    reported_at_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    sla_dead_line_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    dispatch_cost_value = table.Column<decimal>(type: "decimal(65,30)", precision: 65, scale: 30, nullable: false),
                    dispatch_cost_currency = table.Column<string>(type: "longtext", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_emergency_incidents", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_emergency_incidents_guid_code",
                table: "emergency_incidents",
                column: "guid_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emergency_incidents");
        }
    }
}
