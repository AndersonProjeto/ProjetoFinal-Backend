namespace ProjetoBackend.Dominio.DTOs.Evolucao
{
    public class AdicionarEvolucaoDTO
    {
        public int UsuarioId { get; set; }
        public decimal PesoKg { get; set; }
        public decimal? CinturaCm { get; set; }
        public decimal? BracoCm { get; set; }
        public decimal? CoxaCm { get; set; }
    }
}