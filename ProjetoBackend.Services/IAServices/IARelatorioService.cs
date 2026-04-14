using ProjetoBackend.Aplicacao.DTOs.Evolucao;
using ProjetoBackend.Aplicacao.DTOs.Treino;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Services.DtoService;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ProjetoBackend.Services.IAServices
{
    public class IARelatorioService(IConfiguration config)
    {
        private readonly HttpClient _httpClient = new();
        private readonly IConfiguration _config = config;
        private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public async Task<string> GerarRelatorioAsync(
            Usuario usuario,
            int usuarioId,
            IEnumerable<EvolucaoHistoricoDTO> evolucoes,
            IEnumerable<TreinoPorUsuarioDTO> treinos,
            IEnumerable<TreinoResumoDTO> treinosResumo)
        {
            var listaEvolucoes = evolucoes.OrderBy(e => e.DataRegistro).ToList();

            if (listaEvolucoes.Count == 0)
                return JsonSerializer.Serialize(new
                {
                    erro = "Nenhum dado de evolução registrado ainda. Registre seu peso e medidas para gerar um relatório."
                });

            // ── Métricas calculadas no C# (declaradas todas antes de usar) ───────
            var idade = CalcularIdade(usuario.DataNascimento);
            var primeiro = listaEvolucoes.First();
            var ultimo = listaEvolucoes.Last();
            var imc = CalcularIMC(ultimo.PesoKg, usuario.AlturaCm);
            var classificImc = ClassificarIMC(imc);
            var totalDias = (ultimo.DataRegistro - primeiro.DataRegistro).Days;
            var variacaoPeso = ultimo.PesoKg - primeiro.PesoKg;
            var pctPeso = primeiro.PesoKg > 0 ? (variacaoPeso / primeiro.PesoKg) * 100m : 0m;
            var melhorPeso = listaEvolucoes.Min(e => e.PesoKg);
            var piorPeso = listaEvolucoes.Max(e => e.PesoKg);

            var mediaEntreDias = listaEvolucoes.Count > 1
                ? totalDias / (listaEvolucoes.Count - 1)
                : 0;

            var varCintura = (primeiro.CinturaCm.HasValue && ultimo.CinturaCm.HasValue)
                ? (decimal?)(ultimo.CinturaCm.Value - primeiro.CinturaCm.Value) : null;
            var varBraco = (primeiro.BracoCm.HasValue && ultimo.BracoCm.HasValue)
                ? (decimal?)(ultimo.BracoCm.Value - primeiro.BracoCm.Value) : null;
            var varCoxa = (primeiro.CoxaCm.HasValue && ultimo.CoxaCm.HasValue)
                ? (decimal?)(ultimo.CoxaCm.Value - primeiro.CoxaCm.Value) : null;

            // ── Inferência de objetivo (depende de variacaoPeso e classificImc) ──
            var tendenciaPeso = variacaoPeso < -1m ? "perda" : variacaoPeso > 1m ? "ganho" : "manutenção";
            var contextoObjetivo = (classificImc, tendenciaPeso) switch
            {
                ("Peso normal", "perda") => "manutenção ou ganho de massa muscular — IMC já está saudável, perder mais não é indicado",
                ("Peso normal", "manutenção") => "manutenção do peso e ganho de massa muscular",
                ("Peso normal", "ganho") => "ganho de massa muscular controlado mantendo IMC saudável",
                ("Abaixo do peso", _) => "ganho de peso saudável e massa muscular",
                ("Sobrepeso", _) => "redução de gordura corporal e melhora da composição",
                ("Obesidade grau I", _) => "redução de gordura e melhora da saúde metabólica",
                _ => "melhora da composição corporal e saúde geral"
            };

            // ── Treinos ──────────────────────────────────────────────────────────
            var listaTreinos = treinos.ToList();
            var temTreinos = listaTreinos.Any();
            var totalTreinos = listaTreinos.Count;
            var totalExercicios = treinosResumo.Sum(r => r.TotalExercicios);

            var listaTreinosTexto = temTreinos
                ? string.Join("\n", listaTreinos.Select(t =>
                {
                    var resumo = treinosResumo.FirstOrDefault(r => r.TreinoId == t.TreinoId);
                    var totalEx = resumo?.TotalExercicios ?? 0;
                    return $"  • {t.NomeTreino}: {totalEx} exercício{(totalEx != 1 ? "s" : "")} (criado em {t.DataCriacao:dd/MM/yyyy})";
                }))
                : "  Nenhum treino cadastrado.";

            // ── Histórico formatado ──────────────────────────────────────────────
            var historicoEvolucao = string.Join("\n", listaEvolucoes.Select(e =>
                $"[{e.DataRegistro:dd/MM/yyyy}] Peso: {e.PesoKg}kg" +
                (e.CinturaCm.HasValue ? $" | Cintura: {e.CinturaCm}cm" : "") +
                (e.BracoCm.HasValue ? $" | Braço: {e.BracoCm}cm" : "") +
                (e.CoxaCm.HasValue ? $" | Coxa: {e.CoxaCm}cm" : "")
            ));

            // ── Resumo de métricas para a IA ─────────────────────────────────────
            var resumoMetricas = $"""
                PESO: {primeiro.PesoKg}kg → {ultimo.PesoKg}kg ({(variacaoPeso >= 0 ? "+" : "")}{variacaoPeso:F1}kg / {(pctPeso >= 0 ? "+" : "")}{pctPeso:F1}%)
                IMC ATUAL: {imc:F1} — {classificImc}
                MELHOR PESO: {melhorPeso}kg | MAIOR PESO: {piorPeso}kg
                PERÍODO: {totalDias} dias com {listaEvolucoes.Count} registros (média a cada {mediaEntreDias} dias)
                {(varCintura.HasValue ? $"CINTURA: {primeiro.CinturaCm}cm → {ultimo.CinturaCm}cm ({(varCintura >= 0 ? "+" : "")}{varCintura:F1}cm)" : "")}
                {(varBraco.HasValue ? $"BRAÇO:   {primeiro.BracoCm}cm → {ultimo.BracoCm}cm ({(varBraco >= 0 ? "+" : "")}{varBraco:F1}cm)" : "")}
                {(varCoxa.HasValue ? $"COXA:    {primeiro.CoxaCm}cm → {ultimo.CoxaCm}cm ({(varCoxa >= 0 ? "+" : "")}{varCoxa:F1}cm)" : "")}
                TREINOS: {(temTreinos ? $"{totalTreinos} treino(s) com {totalExercicios} exercício(s) no total" : "nenhum")}
                """;

            var formatoJson = """
                {
                  "mensagemMotivacional": "",
                  "resumoAtual": {
                    "imc": 0.0,
                    "classificacaoImc": "",
                    "pesoAtual": 0.0,
                    "cintura": null,
                    "braco": null,
                    "coxa": null,
                    "estadoGeral": "",
                    "destaquePositivo": ""
                  },
                  "evolucao": {
                    "pesoInicial": 0.0,
                    "pesoAtual": 0.0,
                    "variacaoPesoKg": 0.0,
                    "variacaoPesoPercent": 0.0,
                    "tendencia": "",
                    "conquistasDestacadas": [""],
                    "descricao": ""
                  },
                  "pontosDeAtencao": [
                    { "titulo": "", "descricao": "", "comoMelhorar": "" }
                  ],
                  "recomendacoes": [
                    { "titulo": "", "descricao": "", "acaoPratica": "" }
                  ],
                  "relacaoComTreinos": {
                    "analise": "",
                    "sugestoes": [""]
                  },
                  "proximoObjetivo": ""
                }
                """;

            var prompt = $"""
                Você é o AcadIA, personal trainer digital de elite especializado em musculação e nutrição esportiva.
                Analise os dados REAIS do usuário abaixo e gere um relatório técnico, honesto e motivador.

                REGRAS INEGOCIÁVEIS:
                - Use SEMPRE os números exatos fornecidos. Nunca invente ou arredonde diferente.
                - Seja caloroso e motivador — mas elogie com dados concretos, nunca com frases vazias.
                - Para cada ponto negativo, ofereça solução prática aplicável hoje.
                - {(temTreinos ? "O usuário TEM treinos cadastrados. NUNCA diga que ele não treina. Cite os treinos pelo nome." : "O usuário não tem treinos cadastrados ainda.")}

                ═══════════════════════════════════
                PERFIL
                ═══════════════════════════════════
                Nome:   {usuario.Nome}
                Idade:  {idade} anos
                Altura: {usuario.AlturaCm} cm

                ═══════════════════════════════════
                MÉTRICAS CALCULADAS (use esses valores exatos)
                ═══════════════════════════════════
                {resumoMetricas}

                ═══════════════════════════════════
                HISTÓRICO COMPLETO DE EVOLUÇÃO
                ═══════════════════════════════════
                {historicoEvolucao}

                ═══════════════════════════════════
                TREINOS CADASTRADOS
                ═══════════════════════════════════
                {listaTreinosTexto}

                ═══════════════════════════════════
                INSTRUÇÕES DETALHADAS
                ═══════════════════════════════════

                1. mensagemMotivacional
                   Personalize para {usuario.Nome}. Mencione um número real da evolução.

                2. resumoAtual
                   Use os valores exatos. Em "destaquePositivo" cite algo concreto dos dados.

                3. evolucao
                   Preencha com os números exatos das métricas.
                   "tendencia": "Perda de peso", "Ganho de peso" ou "Estável".
                   "conquistasDestacadas": 1 a 3 conquistas reais e mensuráveis.

                4. pontosDeAtencao (máximo 3)
                   {(temTreinos
                       ? "O usuário TEM treinos. NÃO coloque 'falta de treino'. Foque em: frequência de registro, nutrição ou recuperação."
                       : "Inclua falta de treino com sugestão de como começar.")}
                   Cada item DEVE ter "comoMelhorar" com dica concreta.

                5. recomendacoes (3 a 5 itens)
                   {(temTreinos
                       ? $"Pelo menos 2 recomendações devem ser sobre os treinos ({string.Join(", ", listaTreinos.Select(t => t.NomeTreino))}). Sugira progressão de carga, grupos musculares, frequência ideal."
                       : "Pelo menos 1 recomendação sobre como montar uma rotina simples.")}
                   Cada "acaoPratica" deve ser ultra-específico: com número, frequência ou carga.

                6. relacaoComTreinos
                   {(temTreinos
                       ? $"Comente cada treino pelo nome. Relacione com a evolução física. Sugira 3 otimizações específicas para os treinos ({string.Join(", ", listaTreinos.Select(t => t.NomeTreino))})."
                       : "Explique por que treinar é fundamental. Sugira como começar: tipo, frequência e duração para iniciantes.")}

                7. proximoObjetivo
                   OBJETIVO INFERIDO PELOS DADOS: {contextoObjetivo}
                   IMC ATUAL: {imc:F1} ({classificImc})

                   REGRA CRÍTICA DE COERÊNCIA:
                   - IMC normal ({imc:F1}) com perda de peso recente → NÃO peça para perder mais peso. Foque em massa muscular, performance ou força.
                   - IMC sobrepeso/obesidade → perda máxima de 0,5 a 1kg/semana (2 a 4kg em 4 semanas).
                   - IMC abaixo do peso → foque em ganho saudável.

                   Formato obrigatório: [Verbo concreto] + [meta numérica coerente com o IMC] + [prazo de 4 semanas] + [estratégia específica citando os treinos cadastrados por nome]

                   Exemplo CORRETO para IMC normal com perda recente:
                   "Manter o peso em {ultimo.PesoKg}kg (±1kg) nas próximas 4 semanas e aumentar a carga nos treinos cadastrados em 2,5kg por semana, chegando com mais força e definição no final do mês."

                   O objetivo deve soar como escrito por um personal trainer que conhece {usuario.Nome} há meses.

                ═══════════════════════════════════
                FORMATO DE RESPOSTA
                ═══════════════════════════════════
                Retorne SOMENTE o JSON abaixo preenchido. Sem texto antes ou depois. Sem markdown:

                {formatoJson}
                """;

            return await ChamarGroqAsync(prompt);
        }

        private async Task<string> ChamarGroqAsync(string prompt)
        {
            var url = _config["Groq:ApiUrl"];
            var token = _config["Groq:Token"];

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var requestBody = new
            {
                model = "llama-3.3-70b-versatile",
                messages = new[] { new { role = "user", content = prompt } },
                max_tokens = 2000,
                response_format = new { type = "json_object" }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Erro ao gerar relatório: {response.StatusCode} - {error}";
            }

            var result = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<GroqResponse>(result, _jsonOptions);

            return data?.Choices?.FirstOrDefault()?.Message?.Content
                   ?? "IA não retornou resposta.";
        }

        private static int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }

        private static decimal CalcularIMC(decimal pesoKg, decimal alturaCm)
        {
            var m = alturaCm / 100m;
            return pesoKg / (m * m);
        }

        private static string ClassificarIMC(decimal imc) => imc switch
        {
            < 18.5m => "Abaixo do peso",
            >= 18.5m and < 25m => "Peso normal",
            >= 25m and < 30m => "Sobrepeso",
            >= 30m and < 35m => "Obesidade grau I",
            >= 35m and < 40m => "Obesidade grau II",
            _ => "Obesidade grau III"
        };
    }
}