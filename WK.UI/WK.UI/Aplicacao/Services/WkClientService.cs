using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Runtime;
using System.Text.Json;
using System.Text;
using WK.UI.Aplicacao.Interfaces;
using WK.UI.Models.Views;

namespace WK.UI.Aplicacao.Services
{
    public class WkClientService : IWkClientService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;

        public WkClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<string> GetDataAsync(string metodo)
        {
            try
            {
                // URL da API
                string url = $"{_configuration["AppSettings:WKAPI"]}{metodo}";

                // Fazendo a requisição GET
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Lança uma exceção se não for 2xx

                // Lendo o conteúdo da resposta como string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Mostrando o resultado
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Erro ao fazer a requisição: " + e.Message);
                return null;
            }
        }

        public async Task<bool> PostDataAsync<T>(string metodo, T dados)
        {
            var jsonContent = JsonSerializer.Serialize(dados);

            // Cria o conteúdo da requisição com o JSON serializado
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Faz o POST request para a API (substitua a URL pela sua)
            var response = await client.PostAsync($"{_configuration["AppSettings:WKAPI"]}{metodo}", content);

            // Verifica se a requisição foi bem-sucedida
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PutDataAsync<T>(string metodo, T dados)
        {
            var jsonContent = JsonSerializer.Serialize(dados);

            // Cria o conteúdo da requisição com o JSON serializado
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Faz o POST request para a API (substitua a URL pela sua)
            var response = await client.PutAsync($"{_configuration["AppSettings:WKAPI"]}{metodo}", content);

            // Verifica se a requisição foi bem-sucedida
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDataAsync(string metodo)
        {
            var response = await client.DeleteAsync($"{_configuration["AppSettings:WKAPI"]}{metodo}");

            return response.IsSuccessStatusCode;
        }

    }
}
