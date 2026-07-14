namespace ProjetoBackend.Aplicacao.DTOs.Usuario
{
    public class AdicionarUsuarioDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public decimal AlturaCm { get; set; }
        public string AvatarSeed { get; set; } = string.Empty;
        public string AvatarEstilo { get; set; } = string.Empty;
    }
}
