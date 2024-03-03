using Microsoft.Extensions.Configuration;
using Models.Dto;
using Newtonsoft.Json;

namespace Services.RainfallService
{
    public class RainfallService : IRainfallService
    {
        private readonly HttpClient _httpClient;

        private readonly string _baseUrl;
        private readonly string _parameter;

        public RainfallService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();

            _baseUrl = configuration["RainfallApiConfig:BaseUrl"];
            _parameter = configuration["RainfallApiConfig:Parameter"];
        }

        public async Task<List<RainfallReadingDto>> GetRainfallByStation(string station, int count = 10)
        {
            var result = new List<RainfallReadingDto>();

            string url = $"{_baseUrl}/id/stations/{station}/readings?_sorted&_limit={count}&parameter={_parameter}";
            var response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<RainfallReadingDto>(stringResponse);

                foreach (var i in items.Items)
                {
                    result.Add(new RainfallReadingDto()
                    {
                        DateMeasured = i.DateTime,
                        AmountMeasured = i.Value,
                    });
                }
            }

            return result;
        }
    }
}
