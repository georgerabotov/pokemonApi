using FluentAssertions;
using PokeApi.Persistence;
using PokeApi.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Api.IntegrationTests.GetPokemonData
{
    [Trait("Category", "Integration Tests")]
    public class GetPokemonDataServiceTests
    {
        private readonly PokemonAppSettings _settings;
        public GetPokemonDataServiceTests()
        {
            _settings = new PokemonAppSettings(
                "en",
                "cave",
                "Yoda",
                "Shakespeakere",
                "https://api.funtranslations.com/translate/yoda.json?",
                "https://api.funtranslations.com/translate/shakespeare.json?");
        }

        [Fact]
        public async Task Get_Pokemon_Service_Should_Grab_Data_From_API()
        {
            var service = new GetPokemonDataService(_settings);

            var result = await service.GetPokemonAsync("pikachu");

            result.Should().NotBeNull();
            result.Description.Should().NotBeNullOrWhiteSpace();
            result.Habitat.Should().NotBeNullOrWhiteSpace();
            result.Name.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task Get_Pokemon_Service_Should_Throw_When_Pokemon_Is_Null()
        {
            var service = new GetPokemonDataService(_settings);

            Func<Task> act = async () => await service.GetPokemonAsync("adgaerdgaerdg");

            await act.Should().ThrowExactlyAsync<HttpRequestException>().WithMessage("Response status code does not indicate success: 404 (Not Found).");
        }
    }
}
