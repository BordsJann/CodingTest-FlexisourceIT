using Models.Dto;

namespace Services.RainfallService
{
    public interface IRainfallService
    {
        Task<List<RainfallReadingDto>> GetRainfallByStation(string station, int count = 10);
    }
}
