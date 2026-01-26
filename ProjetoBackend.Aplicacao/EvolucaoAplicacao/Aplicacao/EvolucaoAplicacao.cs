using ProjetoBackend.Aplicacao.DTOs.Evolucao;
using ProjetoBackend.Aplicacao.EvolucaoAplicacao.Interface;
using ProjetoBackend.Dominio.DTOs.Evolucao;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Aplicacao.EvolucaoAplicacao.Aplicacao
{
    public class EvolucaoAplicacao : IEvolucaoAplicacao
    {
        private readonly IEvolucaoRepositorio _evolucaoRepositorio;

        public EvolucaoAplicacao(IEvolucaoRepositorio evolucaoRepositorio)
        {
            _evolucaoRepositorio = evolucaoRepositorio;
        }

        public async Task<int> AdicionarEvolucao(AdicionarEvolucaoDTO adicionarEvolucaoDTO)
        {
            var evolucao = new Evolucao(
                adicionarEvolucaoDTO.UsuarioId,
                adicionarEvolucaoDTO.PesoKg,
                adicionarEvolucaoDTO.CinturaCm,
                adicionarEvolucaoDTO.BracoCm,
                adicionarEvolucaoDTO.CoxaCm
            );

            return await _evolucaoRepositorio.AdicionarEvolucao(evolucao);
        }

        public async Task AtualizarEvolucao(AtualizarEvolucaoDTO atualizarEvolucaoDTO)
        {
            var evolucaoExistente = await _evolucaoRepositorio.ObterPorId(atualizarEvolucaoDTO.EvolucaoId);

            if (evolucaoExistente == null)
                throw new Exception("Evolução não encontrada.");

            evolucaoExistente.Atualizar(
                atualizarEvolucaoDTO.PesoKg,
                atualizarEvolucaoDTO.CinturaCm,
                atualizarEvolucaoDTO.BracoCm,
                atualizarEvolucaoDTO.CoxaCm
            );

            await _evolucaoRepositorio.AtualizarEvolucao(evolucaoExistente);
        }

        public async Task<Evolucao?> ObterUltimaEvolucao(int usuarioId)
        {
            if (usuarioId <= 0)
                throw new ArgumentException("Usuário inválido.");

            return await _evolucaoRepositorio.ObterUltimaEvolucao(usuarioId);
        }

        public async Task<EvolucaoResumoDTO?> ResumoEvolucao(int usuarioId)
        {
           
            var resumo = await _evolucaoRepositorio.ResumoEvolucao(usuarioId);
            if (resumo.IMC != null)
            {
                var (classificacao, explicacao) = ImcService.ClassificarImc(resumo.IMC.Value);
                resumo.ImcClassificacao = classificacao;
                resumo.ImcExplicacao = explicacao;
            }

            return resumo;
        }

        public async Task<IEnumerable<EvolucaoHistoricoDTO?>> HistoricoDeEvolucaoDoUsuario(int usuarioId)
        {
            return await _evolucaoRepositorio.HistoricoDeEvolucaoDoUsuario(usuarioId);
        }

        public async Task<decimal> ObterPesoInicial(int usuarioId)
        {
            return await _evolucaoRepositorio.ObterPesoInicial(usuarioId);
        }

        public async Task<decimal> ObterDiferencaPeso(int usuarioId)
        {
            return await _evolucaoRepositorio.ObterDiferencaPeso(usuarioId);
        }
        public async Task<decimal?> ObterCinturaInicial(int usuarioId)
        {
            return await _evolucaoRepositorio.ObterCinturaInicial(usuarioId);
        }

        public async Task<decimal?> ObterDiferencaCintura(int usuarioId)
        {
            return await _evolucaoRepositorio.ObterDiferencaCintura(usuarioId);
        }

        public async Task<decimal?> ObterBracoInicial(int usuarioId)
        {
            return await _evolucaoRepositorio.ObterBracoInicial(usuarioId);
        }

        public async Task<decimal?> ObterDiferencaBraco(int usuarioId)
        {
            return await _evolucaoRepositorio.ObterDiferencaBraco(usuarioId);
        }

        public async Task<decimal?> ObterCoxaInicial(int usuarioId)
        {
            return await _evolucaoRepositorio.ObterCoxaInicial(usuarioId);
        }

        public async Task<decimal?> ObterDiferencaCoxa(int usuarioId)
        {
            return await _evolucaoRepositorio.ObterDiferencaCoxa(usuarioId);
        }
        public static class ImcService
        {
            public static (string Classificacao, string Explicacao) ClassificarImc(decimal imc)
            {
                if (imc < 18.5m)
                    return (
                        "Magreza",
                        "Seu IMC está abaixo do recomendado para a sua altura. Isso pode indicar baixo peso corporal, o que pode afetar energia, imunidade e massa muscular.\n\n" +
                        "O que pode estar acontecendo:\n" +
                        "- Você não está consumindo calorias suficientes\n" +
                        "- Pode estar treinando demais sem repor energia\n" +
                        "- Alimentação com pouca proteína ou carboidratos\n" +
                        "- Estresse ou falta de sono\n\n" +
                        "Como melhorar:\n" +
                        "- Aumente o consumo de alimentos nutritivos (proteína, carboidratos saudáveis e gorduras boas)\n" +
                        "- Faça refeições regulares e reforços (lanches)\n" +
                        "- Priorize proteína em todas as refeições\n" +
                        "- Treine com foco em ganhar massa muscular\n" +
                        "- Procure um nutricionista para um plano personalizado\n\n" +
                        "Objetivo: ganhar peso de forma saudável, com mais energia e mais força."
                    );

                if (imc < 24.9m)
                    return (
                        "Normal",
                        "Seu IMC está dentro da faixa saudável. Isso significa que seu peso está adequado para sua altura e geralmente está associado a mais energia, melhor desempenho físico e menor risco de doenças.\n\n" +
                        "O que isso significa para você:\n" +
                        "- Seu corpo está em equilíbrio\n" +
                        "- Você provavelmente tem boa disposição e recuperação\n" +
                        "- Você está no caminho certo com alimentação e treino\n\n" +
                        "Como manter:\n" +
                        "- Continue treinando com regularidade\n" +
                        "- Mantenha uma alimentação equilibrada e variada\n" +
                        "- Durma bem (7–9 horas)\n" +
                        "- Monitore sua evolução (peso, medidas e força)\n\n" +
                        "Objetivo: manter a saúde e melhorar performance, sem perder o equilíbrio."
                    );

                if (imc < 29.9m)
                    return (
                        "Sobrepeso",
                        "Seu IMC indica sobrepeso. Isso não significa que você está “errado”, mas que seu corpo está carregando mais peso do que o ideal. Isso pode aumentar o risco de doenças ao longo do tempo e também pode afetar seu rendimento e bem-estar.\n\n" +
                        "O que pode estar acontecendo:\n" +
                        "- Excesso de calorias ao longo do tempo\n" +
                        "- Consumo alto de alimentos ultraprocessados\n" +
                        "- Pouca atividade física\n" +
                        "- Falta de sono ou estresse\n\n" +
                        "Como melhorar:\n" +
                        "- Ajuste a alimentação com foco em qualidade (menos ultraprocessados)\n" +
                        "- Aumente atividade física, principalmente treino de força + cardio leve\n" +
                        "- Crie um déficit calórico moderado (sem passar fome)\n" +
                        "- Faça acompanhamento com nutricionista\n\n" +
                        "Objetivo: reduzir gordura corporal e melhorar energia, saúde e disposição."
                    );

                if (imc < 34.9m)
                    return (
                        "Obesidade grau I",
                        "Seu IMC está em obesidade grau I. Isso é um sinal de alerta para a saúde, porque o excesso de peso já pode começar a impactar órgãos, coração e metabolismo.\n\n" +
                        "O que pode acontecer nessa faixa:\n" +
                        "- Aumento de pressão arterial\n" +
                        "- Aumento do risco de diabetes\n" +
                        "- Fadiga e menor disposição\n" +
                        "- Dificuldade de manter ritmo de treino\n\n" +
                        "Como melhorar:\n" +
                        "- Procure um médico para avaliar sua saúde geral\n" +
                        "- Comece um plano alimentar com um nutricionista\n" +
                        "- Treine com foco em consistência (não intensidade)\n" +
                        "- Priorize atividades que você consiga manter semanalmente\n\n" +
                        "Objetivo: reduzir peso com segurança, melhorando saúde e qualidade de vida."
                    );

                if (imc < 39.9m)
                    return (
                        "Obesidade grau II",
                        "Seu IMC está em obesidade grau II, indicando um nível elevado de excesso de peso. Isso pode causar impactos significativos na saúde, como diabetes, hipertensão e problemas articulares.\n\n" +
                        "O que pode acontecer nessa faixa:\n" +
                        "- Maior risco de diabetes tipo 2\n" +
                        "- Aumento de colesterol e pressão\n" +
                        "- Problemas articulares e dor\n" +
                        "- Apneia do sono e falta de ar\n\n" +
                        "Como melhorar:\n" +
                        "- Acompanhamento médico e nutricional é essencial\n" +
                        "- Comece com metas pequenas e realistas (ex: 0,5kg por semana)\n" +
                        "- Faça exercícios de baixo impacto (caminhada, bicicleta, natação)\n" +
                        "- Foque em consistência e mudanças sustentáveis\n\n" +
                        "Objetivo: reduzir peso de forma gradual, proteger a saúde e recuperar qualidade de vida."
                    );

                return (
                    "Obesidade grau III",
                    "Seu IMC está em obesidade grau III, o que representa um alto risco à saúde. Isso exige atenção imediata e acompanhamento profissional, porque o corpo já está sobrecarregado e há maior chance de complicações.\n\n" +
                    "O que pode acontecer nessa faixa:\n" +
                    "- Alto risco de doenças cardíacas e diabetes\n" +
                    "- Dificuldade de mobilidade e fadiga\n" +
                    "- Problemas respiratórios\n" +
                    "- Comprometimento de qualidade de vida\n\n" +
                    "Como melhorar:\n" +
                    "- Procure um médico e nutricionista urgentemente\n" +
                    "- Faça um plano de redução de peso seguro e acompanhado\n" +
                    "- Comece com atividades leves e progressivas\n" +
                    "- Priorize saúde e segurança, sem pressa\n\n" +
                    "Objetivo: reduzir peso com segurança, melhorar saúde e evitar complicações."
                );
            }
        }
    }
}