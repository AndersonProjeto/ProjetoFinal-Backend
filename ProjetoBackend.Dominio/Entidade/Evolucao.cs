namespace ProjetoBackend.Dominio.Entidade
{
    public class Evolucao
    {
        public int EvolucaoId { get; private set; }
        public int UsuarioId { get; private set; }

        public decimal PesoKg { get; private set; }
        public decimal? CinturaCm { get; private set; }
        public decimal? BracoCm { get; private set; }
        public decimal? CoxaCm { get; private set; }

        public DateTime DataRegistro { get; private set; }

        public Usuario Usuario { get; private set; }

        protected Evolucao() { }

        public Evolucao(int usuarioId,decimal pesoKg,decimal? cinturaCm,decimal? bracoCm,decimal? coxaCm)
        {
            if (pesoKg <= 0)
                throw new ArgumentException("Peso tem que ser maior que zero");

            UsuarioId = usuarioId;
            PesoKg = pesoKg;
            CinturaCm = cinturaCm;
            BracoCm = bracoCm;
            CoxaCm = coxaCm;
            DataRegistro = DateTime.UtcNow;
        }
    }
}
