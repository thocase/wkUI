using Microsoft.Extensions.Configuration;
using System.Runtime;
using WK.UI.Aplicacao.Interfaces;

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
    }
}
