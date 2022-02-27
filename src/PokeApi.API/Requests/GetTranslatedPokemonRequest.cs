using MediatR;
using PokeApi.API.Responses;

namespace PokeApi.API.Requests
{
    public class GetTranslatedPokemonRequest : IRequest<PokemonResponse>
    {
        public GetTranslatedPokemonRequest(string pokemonName)
        {
            PokemonName = pokemonName.ToLower();
        }
        /// <summary>
        /// Getting the pokemon name for the request to get the data from the pokedex and translate it.
        /// </summary>
        public string PokemonName { get; }
    }
}
