using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMamoreGranito.Migrations
{
    /// <inheritdoc />
    public partial class AddProcessoSerragem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessosSerragem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BlocoId = table.Column<int>(type: "int", nullable: false),
                    EspessuraChapa = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    QuantidadeChapas = table.Column<int>(type: "int", nullable: false),
                    DataProcesso = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Observacoes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessosSerragem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessosSerragem_Blocos_BlocoId",
                        column: x => x.BlocoId,
                        principalTable: "Blocos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessosSerragem_BlocoId",
                table: "ProcessosSerragem",
                column: "BlocoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessosSerragem");
        }
    }
}
