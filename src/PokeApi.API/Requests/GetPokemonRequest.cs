using MediatR;
using PokeApi.API.Responses;

namespace PokeApi.API.Requests
{
    public class GetPokemonRequest : IRequest<PokemonResponse>
    {
        public GetPokemonRequest(string pokemonName)
        {
            PokemonName = pokemonName.ToLower();
        }
        /// <summary>
        /// Getting the pokemon name for the request to get the data from the pokedex.
        /// </summary>
        public string PokemonName { get; }
    }
}
