using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Aplicacao.DTOs.Evolucao
{
    public class EvolucaoResumoDTO
    {
        public int UsuarioId { get; set; }

        public decimal PesoInicial { get; set; }
        public decimal PesoAtual { get; set; }
        public decimal DiferencaPeso { get; set; }

        public decimal CinturaInicial { get; set; }
        public decimal CinturaAtual { get; set; }
        public decimal DiferencaCintura { get; set; }

        public decimal BracoInicial { get; set; }
        public decimal BracoAtual { get; set; }
        public decimal DiferencaBraco { get; set; }

        public decimal CoxaInicial { get; set; }
        public decimal CoxaAtual { get; set; }
        public decimal DiferencaCoxa { get; set; }

        public decimal? IMC { get; set; }
        public string ImcClassificacao { get; set; } = string.Empty;
        public string ImcExplicacao { get; set; } = string.Empty;

        public DateTime DataUltimaEvolucao { get; set; }
    }

}
