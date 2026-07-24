using System.Reflection;

namespace ProjetoBackend.Repositorio.Sql
{
    /// <summary>
    /// Le os scripts PostgreSQL embarcados no assembly (ver EmbeddedResource no
    /// .csproj). As migrations usam isso para aplicar functions e views junto com
    /// as tabelas, evitando um passo manual no editor do Supabase.
    /// </summary>
    public static class ScriptsSql
    {
        private const string Pasta = "ProjetoBackend.Repositorio.Sql.Postgres.";

        public static string Ler(string nomeArquivo)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var recurso = Pasta + nomeArquivo;

            using var stream = assembly.GetManifestResourceStream(recurso)
                ?? throw new InvalidOperationException(
                    $"Script '{recurso}' nao encontrado. Recursos disponiveis: " +
                    string.Join(", ", assembly.GetManifestResourceNames()));

            using var leitor = new StreamReader(stream);
            return leitor.ReadToEnd();
        }
    }
}
