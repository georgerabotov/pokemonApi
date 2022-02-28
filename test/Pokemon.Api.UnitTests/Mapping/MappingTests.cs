using AutoMapper;
using FluentAssertions;
using PokeApi.API.Mapping;
using PokeApi.API.Responses;
using PokeAPI.Domain.Models;
using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Api.UnitTests.Mapping
{
    [Trait("Category", "Unit Tests")]
    public class MappingTests
    {
        private readonly IMapper _mapper;

        public MappingTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(y =>
                {
                    y.AddProfile(new PokemonResponseMapping());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public void Mapper_Behaves_Correctly_When_Mapping_To_Response_Model()
        {
            var pokemon = new PokemonModel("pikachu", "testDescription", "forest", true, new List<PokemonSpeciesFlavorTexts>());
            var result = _mapper.Map<PokemonResponse>(pokemon);

            result.Name.Should().BeEquivalentTo(pokemon.Name);
            result.Habitat.Should().BeEquivalentTo(pokemon.Habitat);
            result.Description.Should().BeEquivalentTo(pokemon.Description);
            result.IsLegendary.Should().Be(pokemon.IsLegendary);
        }
    }
}
