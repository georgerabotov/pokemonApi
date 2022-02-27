using Newtonsoft.Json;


namespace PokeApi.Persistence.FunApi
{
    internal class FunApiResponseModel
    {
        [JsonProperty("contents")]
        public FunApiResponseModelContents? Contents { get; set; }
    }
}
