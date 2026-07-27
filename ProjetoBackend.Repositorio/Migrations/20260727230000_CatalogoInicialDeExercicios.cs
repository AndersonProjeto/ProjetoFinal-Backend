using Microsoft.EntityFrameworkCore.Migrations;
using ProjetoBackend.Repositorio.Sql;

#nullable disable

namespace ProjetoBackend.Repositorio.Migrations
{
    /// <summary>
    /// Popula o catalogo de exercicios com 40 itens cobrindo os 7 grupos musculares.
    ///
    /// O catalogo e global — todo usuario ve os mesmos exercicios — e sem ele a
    /// aplicacao nasce inutil: nao da para montar treino nenhum. Por isso ele vem
    /// junto com o schema, e nao por cadastro manual ou pela API externa paga.
    ///
    /// O script e idempotente (INSERT ... WHERE NOT EXISTS por nome), entao aplicar
    /// em um banco que ja tenha catalogo nao duplica nem sobrescreve edicoes feitas
    /// pela aplicacao.
    /// </summary>
    public partial class CatalogoInicialDeExercicios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(ScriptsSql.Ler("08_catalogo_exercicios.sql"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove apenas os exercicios do catalogo inicial que ninguem usou em
            // treino. Um exercicio ja referenciado em "TreinoExercicios" e mantido:
            // apagá-lo destruiria o treino de um usuario, o que uma reversao de
            // schema nunca deveria fazer.
            migrationBuilder.Sql("""
                DELETE FROM "Exercicios" e
                WHERE e."Nome" IN (
                    'Supino reto com barra', 'Supino inclinado com halteres',
                    'Supino declinado com barra', 'Crucifixo reto com halteres',
                    'Crossover na polia', 'Flexao de bracos',
                    'Barra fixa pronada', 'Puxada frontal na polia',
                    'Remada curvada com barra', 'Remada unilateral com halter',
                    'Remada baixa na polia', 'Pulldown com bracos estendidos',
                    'Agachamento livre', 'Leg press 45', 'Cadeira extensora',
                    'Mesa flexora', 'Levantamento terra romeno', 'Afundo com halteres',
                    'Panturrilha em pe',
                    'Desenvolvimento militar com barra', 'Desenvolvimento com halteres',
                    'Elevacao lateral', 'Elevacao frontal', 'Crucifixo inverso',
                    'Rosca direta com barra', 'Rosca alternada com halteres',
                    'Rosca martelo', 'Rosca scott', 'Rosca concentrada',
                    'Triceps testa com barra W', 'Triceps na polia com corda',
                    'Triceps frances com halter', 'Mergulho entre bancos', 'Triceps coice',
                    'Prancha isometrica', 'Abdominal supra no solo',
                    'Elevacao de pernas suspenso', 'Abdominal infra no solo',
                    'Prancha lateral', 'Abdominal na polia alta'
                )
                AND NOT EXISTS (
                    SELECT 1 FROM "TreinoExercicios" te
                    WHERE te."ExercicioId" = e."ExercicioId"
                );
                """);
        }
    }
}
