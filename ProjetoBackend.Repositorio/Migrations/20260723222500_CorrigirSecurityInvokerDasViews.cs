using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoBackend.Repositorio.Migrations
{
    /// <summary>
    /// Fecha o desvio de RLS pelas views.
    ///
    /// Views no PostgreSQL rodam por padrao com as permissoes do dono. Como foram
    /// criadas por 'postgres', consultá-las via PostgREST ignorava o RLS habilitado
    /// nas tabelas base — expondo "Evolucoes" inteira. security_invoker = true faz a
    /// view rodar como quem consulta, entao o RLS volta a valer.
    ///
    /// Alem disso revoga privilegios de anon/authenticated no schema public: esta
    /// aplicacao nunca usa PostgREST (o React fala com a API .NET, que fala Postgres
    /// direto como 'postgres'), entao esses papeis nao precisam de acesso nenhum.
    /// </summary>
    public partial class CorrigirSecurityInvokerDasViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER VIEW "vwEvolucaoHistorico" SET (security_invoker = true);
                ALTER VIEW "vwEvolucaoResumo"    SET (security_invoker = true);
                """);

            // Os papeis anon/authenticated so existem no Supabase; num Postgres local
            // o REVOKE quebraria a migration, dai a checagem antes.
            migrationBuilder.Sql("""
                DO $$
                DECLARE
                    papel TEXT;
                BEGIN
                    FOREACH papel IN ARRAY ARRAY['anon', 'authenticated']
                    LOOP
                        IF EXISTS (SELECT 1 FROM pg_roles WHERE rolname = papel) THEN
                            EXECUTE format('REVOKE ALL ON ALL TABLES IN SCHEMA public FROM %I;', papel);
                            EXECUTE format('REVOKE ALL ON ALL ROUTINES IN SCHEMA public FROM %I;', papel);
                            EXECUTE format('REVOKE ALL ON ALL SEQUENCES IN SCHEMA public FROM %I;', papel);
                            EXECUTE format('ALTER DEFAULT PRIVILEGES IN SCHEMA public REVOKE ALL ON TABLES FROM %I;', papel);
                            EXECUTE format('ALTER DEFAULT PRIVILEGES IN SCHEMA public REVOKE ALL ON ROUTINES FROM %I;', papel);
                            EXECUTE format('ALTER DEFAULT PRIVILEGES IN SCHEMA public REVOKE ALL ON SEQUENCES FROM %I;', papel);
                        END IF;
                    END LOOP;
                END;
                $$;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER VIEW "vwEvolucaoHistorico" SET (security_invoker = false);
                ALTER VIEW "vwEvolucaoResumo"    SET (security_invoker = false);
                """);

            migrationBuilder.Sql("""
                DO $$
                DECLARE
                    papel TEXT;
                BEGIN
                    FOREACH papel IN ARRAY ARRAY['anon', 'authenticated']
                    LOOP
                        IF EXISTS (SELECT 1 FROM pg_roles WHERE rolname = papel) THEN
                            EXECUTE format('GRANT ALL ON ALL TABLES IN SCHEMA public TO %I;', papel);
                            EXECUTE format('GRANT ALL ON ALL ROUTINES IN SCHEMA public TO %I;', papel);
                            EXECUTE format('GRANT ALL ON ALL SEQUENCES IN SCHEMA public TO %I;', papel);
                        END IF;
                    END LOOP;
                END;
                $$;
                """);
        }
    }
}
