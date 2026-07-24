using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoBackend.Repositorio.Migrations
{
    /// <summary>
    /// Habilita Row Level Security em todas as tabelas do schema public.
    ///
    /// O Supabase publica o schema public como API REST (PostgREST), acessivel por
    /// HTTP com a anon key. Sem RLS, qualquer detentor dessa chave leria e gravaria
    /// direto nas tabelas — incluindo Nome, Email e SenhaHash de "Usuarios".
    ///
    /// Habilitamos RLS SEM criar policy alguma: isso bloqueia por completo os papeis
    /// anon/authenticated usados pelo PostgREST. A API .NET nao e afetada porque
    /// conecta como 'postgres', dono das tabelas, e donos ignoram RLS por padrao
    /// (nao usamos FORCE ROW LEVEL SECURITY).
    /// </summary>
    public partial class HabilitarRlsNasTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Varre o schema em vez de listar nomes: nenhuma tabela escapa, incluindo
            // a __EFMigrationsHistory criada pelo proprio EF.
            migrationBuilder.Sql("""
                DO $$
                DECLARE
                    tabela RECORD;
                BEGIN
                    FOR tabela IN
                        SELECT tablename
                        FROM pg_tables
                        WHERE schemaname = 'public'
                    LOOP
                        EXECUTE format('ALTER TABLE public.%I ENABLE ROW LEVEL SECURITY;', tabela.tablename);
                    END LOOP;
                END;
                $$;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DO $$
                DECLARE
                    tabela RECORD;
                BEGIN
                    FOR tabela IN
                        SELECT tablename
                        FROM pg_tables
                        WHERE schemaname = 'public'
                    LOOP
                        EXECUTE format('ALTER TABLE public.%I DISABLE ROW LEVEL SECURITY;', tabela.tablename);
                    END LOOP;
                END;
                $$;
                """);
        }
    }
}
