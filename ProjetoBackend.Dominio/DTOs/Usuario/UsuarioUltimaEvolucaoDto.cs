namespace ProjetoBackend.Aplicacao.DTOs.Usuario
{
    public class UsuarioUltimaEvolucaoDto
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal? PesoKg { get; set; }
        public DateTime? DataRegistro { get; set; }
    }

}