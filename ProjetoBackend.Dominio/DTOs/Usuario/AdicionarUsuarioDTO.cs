namespace ProjetoBackend.Aplicacao.DTOs.Usuario
{
    public class AdicionarUsuarioDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal AlturaCm { get; set; }
        public string AvatarSeed { get; set; }
        public string AvatarEstilo { get; set; }
    }
}
