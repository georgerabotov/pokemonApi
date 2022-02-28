using AutoMapper;
using FluentAssertions;
using NSubstitute;
using PokeApi.API.Handlers;
using PokeApi.API.Mapping;
using PokeApi.API.Requests;
using PokeApi.API.Responses;
using PokeAPI.Domain;
using PokeAPI.Domain.Models;
using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Api.UnitTests.Handlers
{
    [Trait("Category", "Unit Tests")]
    public class RequestHandlerTests
    {
        private readonly IGetPokemonData _pokemonDataService;
        private readonly IMapper _mapper;
        private readonly ITranslateDescription _translator;

        public RequestHandlerTests()
        {
            _pokemonDataService = Substitute.For<IGetPokemonData>();
            _translator = Substitute.For<ITranslateDescription>();
            _mapper = new MapperConfiguration(x => x.AddProfile(new PokemonResponseMapping())).CreateMapper();
        }

        [Fact]
        public async Task PokemonRequest_Handle_Should_Return_An_PokemonResponse_Object_After_Mapping()
        {
            var service = new GetPokemonRequestHandler(_pokemonDataService, _mapper);
            var expectedObjectFromService = new PokemonModel("pikachu", "testDescription", "forest", true, new List<PokemonSpeciesFlavorTexts>());
            _pokemonDataService.GetPokemonAsync(Arg.Any<string>()).Returns(expectedObjectFromService);

            var result = await service.Handle(new GetPokemonRequest("pikachu"), CancellationToken.None);

            result.Should().BeOfType(typeof(PokemonResponse));
            result.Name.Should().BeEquivalentTo(expectedObjectFromService.Name);
            result.Habitat.Should().BeEquivalentTo(expectedObjectFromService.Habitat);
            result.Description.Should().BeEquivalentTo(expectedObjectFromService.Description);
            result.IsLegendary.Should().Be(expectedObjectFromService.IsLegendary);
        }

        [Fact]
        public async Task PokemonTranslatedRequest_Handle_Should_Return_An_PokemonResponse_Object_After_Mapping()
        {
            var service = new GetTranslatedPokemonRequestHandler(_pokemonDataService, _translator, _mapper);
            var expectedObjectFromService = new PokemonModel("pikachu", "testDescription", "forest", true, new List<PokemonSpeciesFlavorTexts>());
            _pokemonDataService.GetPokemonAsync(Arg.Any<string>()).Returns(expectedObjectFromService);
            _translator.TranslatePokemonDescriptionAsync(Arg.Any<PokemonModel>()).Returns("new test description");
            var result = await service.Handle(new GetTranslatedPokemonRequest("pikachu"), CancellationToken.None);

            result.Should().BeOfType(typeof(PokemonResponse));
            result.Name.Should().BeEquivalentTo(expectedObjectFromService.Name);
            result.Habitat.Should().BeEquivalentTo(expectedObjectFromService.Habitat);
            result.Description.Should().BeEquivalentTo("new test description");
            result.IsLegendary.Should().Be(expectedObjectFromService.IsLegendary);
        }
    }
}
