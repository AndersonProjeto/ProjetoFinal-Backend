namespace ProjetoBackend.Dominio
{
    public class IAInteracao
    {
        public int IAInteracaoId { get; private set; }
        public string Pergunta { get; private set; }
        public string Resposta { get; private set; }
        public DateTime DataHora { get; private set; }
        public Usuario Usuario { get; private set; }
        public int UsuarioId { get; private set; }

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