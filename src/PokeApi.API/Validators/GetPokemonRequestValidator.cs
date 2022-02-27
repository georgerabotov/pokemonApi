using FluentValidation;
using PokeApi.API.Requests;

namespace PokeApi.API.Validators
{
    public class GetPokemonRequestValidator : AbstractValidator<GetPokemonRequest>
    {
        public GetPokemonRequestValidator()
        {
            RuleFor(x => x.PokemonName).NotNull().NotEmpty();
        }
    }
}
