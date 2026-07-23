namespace ProjetoBackend.Aplicacao.DTOs.Usuario
{
    public class UsuarioResumoDto
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal AlturaCm { get; set; }
        public decimal? PesoKg { get; set; }
        public int Idade { get; set; }
        public decimal? IMC { get; set; }
        public DateTime DataCriacao { get; set; }
    }

}