using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace xmapproject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "process",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    lastEdiTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    userLastEdited = table.Column<string>(type: "text", nullable: true),
                    urlProcess = table.Column<string>(type: "text", nullable: true),
                    xmlDiagrama = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_process", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "atividade",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nomeAtividade = table.Column<string>(type: "text", nullable: true),
                    processId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividade", x => x.id);
                    table.ForeignKey(
                        name: "FK_atividade_process_processId",
                        column: x => x.processId,
                        principalTable: "process",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "metaDados",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dados = table.Column<string>(type: "text", nullable: false),
                    ator = table.Column<string>(type: "text", nullable: false),
                    lgpd = table.Column<string>(type: "text", nullable: false),
                    atividadeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metaDados", x => x.id);
                    table.ForeignKey(
                        name: "FK_metaDados_atividade_atividadeId",
                        column: x => x.atividadeId,
                        principalTable: "atividade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_atividade_processId",
                table: "atividade",
                column: "processId");

            migrationBuilder.CreateIndex(
                name: "IX_metaDados_atividadeId",
                table: "metaDados",
                column: "atividadeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "metaDados");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "atividade");

            migrationBuilder.DropTable(
                name: "process");
        }
    }
}
