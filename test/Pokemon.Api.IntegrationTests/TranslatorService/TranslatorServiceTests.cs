using FluentAssertions;
using PokeApi.Persistence;
using PokeApi.Persistence.Settings;
using PokeAPI.Domain.Models;
using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Api.UnitTests.TranslatorService
{
    [Trait("Category", "Integration Tests")]
    public class TranslatorServiceTests
    {
        private readonly PokemonAppSettings _settings;
        public TranslatorServiceTests()
        {
            _settings = new PokemonAppSettings(
                "en",
                "cave",
                "Yoda",
                "Shakespeakere",
                "https://api.funtranslations.com/translate/yoda.json?",
                "https://api.funtranslations.com/translate/shakespeare.json?");
        }

        [Theory]
        [InlineData("zapdos", "this is a test description", "forest", false, "This is a new flavour text")]
        [InlineData("zapdos", "this is a test description", "cave", false, "A new flavour text,  this is")]
        [InlineData("zapdos", "this is a test description", "forest", true, "A new flavour text,  this is")]
        public async Task Validate_Service_Behaves_Correctly(string name, string description, string habitat, bool islegendary, string expectedObject)
        {
            // sometimes these fail because the API can only take (x) amount of requests
            var service = new TranslateDescriptionService(_settings);

            var result = await service.TranslatePokemonDescriptionAsync
                (new PokemonModel
                    (name, description, habitat, islegendary, new List<PokemonSpeciesFlavorTexts> 
                        { new PokemonSpeciesFlavorTexts 
                            { Language = new NamedApiResource<Language> 
                                { Name = "en" }, FlavorText = "this is a new flavour Text" 
                            } 
                        }
                    )
                );

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().NotBe("this is a test description");
            result.Should().BeEquivalentTo(expectedObject);
        }
    }
}
