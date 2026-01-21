using ProjetoBackend.Dominio.DTOs.Exercicio;
using ProjetoBackend.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBackend.Aplicacao.ExercicioAplicacao.Aplicacao
{
    public class ExerciseDbService
    {
        private readonly HttpClient _httpClient;

        public ExerciseDbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static readonly Dictionary<EnumGrupoMuscular, string> MapaGrupoMuscular =
            new()
        {
            { EnumGrupoMuscular.Peito, "chest" },
            { EnumGrupoMuscular.Costas, "back" },
            { EnumGrupoMuscular.Pernas, "legs" },
            { EnumGrupoMuscular.Ombros, "shoulders" },
            { EnumGrupoMuscular.Biceps, "upper arms" },
            { EnumGrupoMuscular.Triceps, "upper arms" },
            { EnumGrupoMuscular.Abdomen, "waist" }
        };

        private static string ConverterGrupoMuscular(EnumGrupoMuscular grupoMuscular)
        {
            if (!MapaGrupoMuscular.TryGetValue(grupoMuscular, out var grupoEn))
                throw new Exception("Grupo muscular não mapeado.");

            return grupoEn;
        }

        public async Task<string?> BuscarImagemAsync(EnumGrupoMuscular grupoMuscular)
        {
            var grupoEn = ConverterGrupoMuscular(grupoMuscular);

            var response = await _httpClient.GetAsync($"/api/v1/exercises/search?search={grupoEn}");

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao buscar imagem: {response.StatusCode}");

            var data = await response.Content.ReadFromJsonAsync<ExerciseAscendResponse>();

            // Aqui você pega o primeiro item da lista
            return data?.Data?.FirstOrDefault()?.ImageUrl;
        }



    }
}
