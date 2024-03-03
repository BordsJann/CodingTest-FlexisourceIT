using CodingTest_FlexisourceIT.Controllers;
using Services.RainfallService;
using Moq;
using Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace CodingTest_FlexisourceIT.Tests
{
    [TestClass]
    public class TestRainfallController
    {
        [TestMethod]
        public async Task GetAllRainfallByStation_ShouldReturnAtleastOneOrMoreReadings ()
        {
            var expected = new List<RainfallReadingDto>()
            {
                new RainfallReadingDto
                {
                    DateMeasured = DateTime.Now,
                    AmountMeasured = 0
                }
            };

            var rainfallServiceMock = new Mock<IRainfallService>();

            rainfallServiceMock.Setup(m => m.GetRainfallByStation("3680", 10)).ReturnsAsync(expected).Verifiable();

            var controller = new RainfallController(rainfallServiceMock.Object);
            var expectedResult = await controller.GetRainfallReadingsByStation("3680");

            var inMemorySettings = new Dictionary<string, string> {
                {"RainfallApiConfig:BaseUrl", "https://environment.data.gov.uk/flood-monitoring"},
                {"RainfallApiConfig:Parameter", "rainfall"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var rainfallService = new RainfallService(configuration);

            var actualResult = await rainfallService.GetRainfallByStation("3680", 10);

            Assert.IsNotNull(expectedResult);

            var expectedResponseInModel = (expectedResult as ObjectResult).Value as ResponseDto;
            var expectedResultInModel = JsonConvert.DeserializeObject<List<RainfallReadingDto>>(expectedResponseInModel.RainfallReadingResponse.Content);

            Assert.IsNull(expectedResponseInModel.ErrorResponse);
            Assert.IsTrue(expectedResultInModel?.Count > 0 && actualResult.Count > 0);
        }


    }
}