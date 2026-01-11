using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Aplicacao.DTOs.Evolucao
{
    public class EvolucaoResumoDTO
    {
        public int UsuarioId { get; set; }
        public decimal PesoAtual { get; set; }
        public decimal PesoInicial { get; set; }
        public decimal DiferencaPeso { get; set; }
        public DateTime DataUltimaEvolucao { get; set; }
    }
}
