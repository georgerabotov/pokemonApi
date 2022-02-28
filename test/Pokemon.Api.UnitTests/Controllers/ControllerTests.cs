using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PokeApi.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace Pokemon.Api.UnitTests.Controllers
{
    [Trait("Category", "Unit Tests")]
    public class ControllerTests
    {
        [Fact]
        public async Task PokemonApiController_GetPokemon_Should_Return_An_Ok()
        {
            //Arrange
            var mediator = Substitute.For<IMediator>();
            var controller = new PokemonController(mediator);
            //Act

            var result = await controller.Get("mewtwo");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task PokemonApiController_GetPokemonTranslated_Should_Return_An_Ok()
        {
            //Arrange
            var mediator = Substitute.For<IMediator>();
            var controller = new PokemonController(mediator);
            //Act

            var result = await controller.GetTranslated("mewtwo");

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    }
}
