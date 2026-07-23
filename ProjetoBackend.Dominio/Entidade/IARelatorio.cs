namespace ProjetoBackend.Dominio.Entidade
{
    public class IARelatorio
    {
        public int IARelatorioId { get; private set; }
        public int UsuarioId { get; private set; }
        public string Relatorio { get; private set; } = string.Empty;
        public DateTime DataGerado { get; private set; }

        public Usuario? Usuario { get; private set; }

        protected IARelatorio() { }

        public IARelatorio(int usuarioId, string relatorio)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");

            if (string.IsNullOrWhiteSpace(relatorio))
                throw new ArgumentException("Relatório não pode ser vazio.");

            UsuarioId = usuarioId;
            Relatorio = relatorio;
            DataGerado = DateTime.UtcNow;
        }
    }
}