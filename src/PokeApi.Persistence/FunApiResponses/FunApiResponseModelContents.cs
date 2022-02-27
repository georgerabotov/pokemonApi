using Newtonsoft.Json;

namespace PokeApi.Persistence.FunApi
{
    internal class FunApiResponseModelContents
    {
        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("translated")]
        public string? Translated { get; set; }
    }
}
