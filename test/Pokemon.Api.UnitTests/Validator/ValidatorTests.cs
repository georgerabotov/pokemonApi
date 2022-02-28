using FluentAssertions;
using FluentValidation.TestHelper;
using PokeApi.API.Requests;
using PokeApi.API.Validators;
using Xunit;

namespace Pokemon.Api.UnitTests.Validator
{
    [Trait("Category", "Unit Tests")]
    public class ValidatorTests
    {
        private GetPokemonRequestValidator _pokemonValidator;
        private GetTranslatedPokemonRequestValidator _translatedPokemonValidator;
        public ValidatorTests()
        {
            _pokemonValidator = new GetPokemonRequestValidator();
            _translatedPokemonValidator = new GetTranslatedPokemonRequestValidator();
        }

        [Fact]
        public void Validator_Should_Have_Error_When_Name_Is_Empty()
        {
            //arrange 
            var model = new GetPokemonRequest("");

            //Act
            var result = _pokemonValidator.TestValidate(model);

            //Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.PokemonName).WithErrorMessage("'Pokemon Name' must not be empty.");
        }

        [Fact]
        public void Validator_Should_Return_Valid_Result_When_No_Validation_Fails()
        {
            //arrange 
            var model = new GetPokemonRequest("mewtwo");

            //Act
            var result = _pokemonValidator.TestValidate(model);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Translate_Validator_Should_Have_Error_When_Name_Is_Empty()
        {
            //arrange 
            var model = new GetTranslatedPokemonRequest("");

            //Act
            var result = _translatedPokemonValidator.TestValidate(model);

            //Assert
            result.IsValid.Should().BeFalse();
            result.ShouldHaveValidationErrorFor(x => x.PokemonName).WithErrorMessage("'Pokemon Name' must not be empty.");
        }

        [Fact]
        public void Translate_Validator_Should_Return_Valid_Result_When_No_Validation_Fails()
        {
            //arrange 
            var model = new GetTranslatedPokemonRequest("pikachu");

            //Act
            var result = _translatedPokemonValidator.TestValidate(model);

            //Assert
            result.IsValid.Should().BeTrue();
        }
    }
}
