namespace ProjetoBackend.Dominio.Entidade
{
    public class IAInteracao
    {
        public int IAInteracaoId { get;  set; }
        public string Pergunta { get;  set; } = string.Empty;
        public string Resposta { get;  set; } = string.Empty;
        public DateTime DataHora { get;  set; }
        public Usuario? Usuario { get;  set; }
        public int UsuarioId { get;  set; }

        protected IAInteracao() { }

        public IAInteracao(int usuarioId,string pergunta,string resposta)
        {
            if (string.IsNullOrWhiteSpace(pergunta))
                throw new ArgumentException("Pergunta é obrigatória.");

            UsuarioId = usuarioId;
            Pergunta = pergunta;
            Resposta = resposta;
            DataHora = DateTime.UtcNow;
        }
    }
}