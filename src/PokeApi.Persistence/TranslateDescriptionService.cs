using Newtonsoft.Json;
using PokeApi.Persistence.FunApi;
using PokeApi.Persistence.Helpers;
using PokeApi.Persistence.Settings;
using PokeAPI.Domain;
using PokeAPI.Domain.Models;
using System.Net;

namespace PokeApi.Persistence
{
    public class TranslateDescriptionService : ITranslateDescription
    {
        private PokemonAppSettings _settings;
        private HttpClient _client;
        public TranslateDescriptionService(PokemonAppSettings settings)
        {
            _settings = settings ?? throw new ArgumentException(nameof(settings));
            _client = new HttpClient();
        }

        public Task<string> TranslatePokemonDescription(PokemonModel pokemon)
        {
            var randomInt = pokemon.RandomTranslationSelector(_settings.Language);
            return Translate(pokemon, randomInt);
        }

        private async Task<string> Translate(PokemonModel pokemon, int random)
        {
            var text = pokemon.FlavorTextEntries[random].FlavorText.Replace("\n", "%20").Replace("\r", "%20").Replace("\f", "%20");
            var result = "";

            var response = (pokemon.Habitat == _settings.CaveHabitat || pokemon.IsLegendary)
                ? await _client.GetAsync(_settings.YodaTranslationUrl + "text=" + text)
                : await _client.GetAsync(_settings.ShakeSpeareTranslationUrl + "text=" + text);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<FunApiResponseModel>(resultString).Contents?.Translated;
            }
            
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new Exception("Too many requests, please try again later");
            }

            return String.IsNullOrWhiteSpace(result) ? String.Empty : result;
        }
    }
}
