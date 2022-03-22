using OvermindDogFacts.Interfaces;
using OvermindDogFacts.Model;
using OvermindDogFacts.Models;
using OvermindDogFacts.Utils;
using System.Text.Json;

namespace OvermindDogFacts.Services
{
    public class DogFactService : IDogFactService
    {
        public async Task ExecuteAsync(ConfigurationFact config)
        {
            if (!Directory.Exists(config.Input))
                throw new System.IO.DirectoryNotFoundException("Diretório de entrada não existe");

            var files = Directory.GetFiles(config.Input, "*.xlsx", SearchOption.AllDirectories);

            if (files.Count() < 1)
                throw new System.IO.FileNotFoundException("Nenhum arquivo Excel encontrado.");

            foreach (var file in files)
            {
                var rows = ExcelUtils.ReadExcel(file);

                List<DogFact> facts = new List<DogFact>();

                foreach (var row in rows)
                {
                    var _facts = await GetFactsAsync(config.EndPoint, int.Parse(row));

                    facts.Add(new DogFact()
                    {
                        dog = int.Parse(row),
                        facts = _facts
                    });
                }

                await ExcelUtils.WriteExcel(facts, $"{config.Output}{Path.DirectorySeparatorChar}{(new FileInfo(file)).Name}");

                File.Move(file, $"{config.Processed}{Path.DirectorySeparatorChar}{(new FileInfo(file)).Name}", true);
            }
        }

        private async Task<ICollection<Fact>> GetFactsAsync(String url, int parameter)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(url);
                var response = await client.GetAsync($"?number={parameter}");

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Erro ao obter DogFacts - {response.Content}.");

                try
                {
                    return await JsonSerializer.DeserializeAsync<ICollection<Fact>>(await response.Content.ReadAsStreamAsync());
                }
                catch (JsonException error)
                {
                    throw error;
                }
            }
        }
    }
}
