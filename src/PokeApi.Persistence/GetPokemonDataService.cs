using PokeApi.Persistence.Settings;
using PokeAPI.Domain;
using PokeAPI.Domain.Models;
using PokeApiNet;

namespace PokeApi.Persistence
{
    public class GetPokemonDataService : IGetPokemonData
    {
        private readonly PokeApiClient _pokeApiClient;
        private readonly PokemonAppSettings _settings;
        public GetPokemonDataService(PokemonAppSettings settings)
        {
            _settings = settings ?? throw new ArgumentException(nameof(settings));
            _pokeApiClient = new PokeApiClient();
        }
        public async Task<PokemonModel> GetPokemonAsync(string pokemonName)
        {
            var pokemonData = await _pokeApiClient.GetResourceAsync<PokemonSpecies>(pokemonName);
            if (pokemonData != null)
            {
                return new PokemonModel(
                                pokemonData.Name,
                                pokemonData.FlavorTextEntries.Find(x => x.Language.Name == _settings.Language).FlavorText.Replace("\n", " ").Replace("\r", " ").Replace("\f", " "),
                                pokemonData.Habitat.Name,
                                pokemonData.IsLegendary,
                                pokemonData.FlavorTextEntries);
            }
            else
            {
                throw new Exception("Could not find the pokemon");
            }
        }
    }
}
