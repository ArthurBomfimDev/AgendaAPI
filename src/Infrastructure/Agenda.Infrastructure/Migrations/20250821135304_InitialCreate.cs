using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agenda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "atividade",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    status_atividade = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    prioridade = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    data_vencimento = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    data_inicio_real = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    data_conclusao_real = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    data_cancelamento_real = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    decorrido_desde_criaçao = table.Column<TimeSpan>(type: "time", nullable: true),
                    tempo_ate_inicio = table.Column<TimeSpan>(type: "time", nullable: true),
                    tempo_ativo_total = table.Column<TimeSpan>(type: "time", nullable: true),
                    duracao_atraso = table.Column<TimeSpan>(type: "time", nullable: true),
                    data_criacao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    data_atualizacao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividade", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "atividade_logs_tempo",
                columns: table => new
                {
                    inicio = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    atividade_Id = table.Column<long>(type: "bigint", nullable: false),
                    fim = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividade_logs_tempo", x => new { x.atividade_Id, x.inicio });
                    table.ForeignKey(
                        name: "FK_atividade_logs_tempo_atividade_atividade_Id",
                        column: x => x.atividade_Id,
                        principalTable: "atividade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atividade_logs_tempo");

            migrationBuilder.DropTable(
                name: "atividade");
        }
    }
}
