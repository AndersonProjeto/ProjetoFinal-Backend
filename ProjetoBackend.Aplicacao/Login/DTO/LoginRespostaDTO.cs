namespace ProjetoBackend.Aplicacao.Login.DTO
{
    public class LoginRespostaDTO
    {
        public required string Token { get; set; }
        public DateTime TempoDeExpirarOToken { get; set; }
        public int UsuarioId { get; set; }
    }
}