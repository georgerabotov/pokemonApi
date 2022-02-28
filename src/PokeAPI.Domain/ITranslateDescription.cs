using PokeAPI.Domain.Models;

namespace PokeAPI.Domain
{
    public interface ITranslateDescription
    {
        Task<string> TranslatePokemonDescriptionAsync(PokemonModel pokemon);
    }
}
