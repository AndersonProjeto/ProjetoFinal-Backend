using Microsoft.EntityFrameworkCore.Migrations;
using ProjetoBackend.Repositorio.Sql;

#nullable disable

namespace ProjetoBackend.Repositorio.Migrations
{
    /// <summary>
    /// Amplia o catalogo com mais 35 exercicios classicos, levando o total a 75.
    ///
    /// Completa os grupos com os movimentos que ficaram de fora da primeira leva —
    /// terra convencional, paralelas, elevacao pelvica, agachamento bulgaro, face
    /// pull, entre outros — para que cada grupo tenha variedade suficiente de
    /// equipamento e angulo para montar treinos diferentes.
    ///
    /// Idempotente por nome, como o catalogo inicial.
    /// </summary>
    public partial class CatalogoExerciciosExtra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(ScriptsSql.Ler("09_catalogo_exercicios_extra.sql"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Preserva exercicios ja usados em treinos: reverter schema nunca deve
            // destruir o treino montado por um usuario.
            migrationBuilder.Sql("""
                DELETE FROM "Exercicios" e
                WHERE e."Nome" IN (
                    'Supino reto com halteres', 'Voador peck deck', 'Supino reto na maquina',
                    'Crucifixo inclinado com halteres', 'Pullover com halter',
                    'Barra fixa supinada', 'Puxada com pegada neutra', 'Remada cavalinho',
                    'Levantamento terra convencional', 'Encolhimento com halteres',
                    'Face pull na polia',
                    'Agachamento hack', 'Elevacao pelvica', 'Agachamento bulgaro',
                    'Cadeira abdutora', 'Panturrilha sentado',
                    'Desenvolvimento Arnold', 'Elevacao lateral na polia',
                    'Remada alta com barra', 'Desenvolvimento na maquina',
                    'Elevacao frontal com barra',
                    'Rosca inversa com barra', 'Rosca na polia baixa',
                    'Rosca no banco inclinado', 'Rosca 21',
                    'Paralelas', 'Triceps na polia com barra reta', 'Supino fechado',
                    'Triceps unilateral na polia', 'Triceps na maquina',
                    'Abdominal bicicleta', 'Russian twist', 'Abdominal canivete',
                    'Roda abdominal', 'Escalador'
                )
                AND NOT EXISTS (
                    SELECT 1 FROM "TreinoExercicios" te
                    WHERE te."ExercicioId" = e."ExercicioId"
                );
                """);
        }
    }
}
