namespace ProjetoBackend.Aplicacao.DTOs.Evolucao
{
    public class EvolucaoHistoricoDTO
    {
        public int EvolucaoId { get; set; }
        public decimal PesoKg { get; set; }
        public decimal? CinturaCm { get; set; }
        public decimal? BracoCm { get; set; }
        public decimal? CoxaCm { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
