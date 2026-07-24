using Microsoft.EntityFrameworkCore.Migrations;
using ProjetoBackend.Repositorio.Sql;

#nullable disable

namespace ProjetoBackend.Repositorio.Migrations
{
    /// <summary>
    /// Corrige o arredondamento de "fnCalcularIMC".
    ///
    /// No SQL Server, RETURNS DECIMAL(5,2) arredondava o valor devolvido. O
    /// PostgreSQL ignora o modificador de escala no tipo de retorno de funcao, entao
    /// a divisao voltava com precisao cheia (27.7777... no lugar de 27.78) e
    /// contaminava tambem a coluna IMC de "vwUsuarioResumo". A correcao aplica
    /// ROUND(...,2) explicito dentro da funcao.
    /// </summary>
    public partial class CorrigirArredondamentoIMC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // CREATE OR REPLACE: reaplicar o script inteiro é idempotente.
            migrationBuilder.Sql(ScriptsSql.Ler("01_usuario.sql"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                CREATE OR REPLACE FUNCTION "fnCalcularIMC"(
                    p_peso_kg   NUMERIC(5,2),
                    p_altura_cm NUMERIC(5,2)
                )
                RETURNS NUMERIC(5,2)
                LANGUAGE sql
                IMMUTABLE
                SET search_path = public
                AS $$
                    SELECT CASE
                        WHEN p_altura_cm IS NULL OR p_altura_cm <= 0 THEN NULL
                        ELSE p_peso_kg / POWER(p_altura_cm / 100.0, 2)
                    END;
                $$;
                """);
        }
    }
}
