using FluentValidation;
using PokeApi.API.Requests;

namespace PokeApi.API.Validators
{
    public class GetTranslatedPokemonRequestValidator : AbstractValidator<GetTranslatedPokemonRequest>
    {
        public GetTranslatedPokemonRequestValidator()
        {
            RuleFor(x => x.PokemonName).NotNull().NotEmpty();
        }
    }
}
