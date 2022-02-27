using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.Persistence.Settings
{
    public class PokemonAppSettings
    {
        public PokemonAppSettings(
            string language,
            string caveHabitat,
            string yodaType,
            string shakespeareType,
            string yodaTranslationUrl,
            string shakespeareTranslationUrl)
        {
            Language = !string.IsNullOrWhiteSpace(language) ? language : throw new ArgumentException(nameof(language));
            CaveHabitat = !string.IsNullOrWhiteSpace(caveHabitat) ? caveHabitat : throw new ArgumentException(nameof(caveHabitat));
            YodaType = !string.IsNullOrWhiteSpace(yodaType) ? yodaType : throw new ArgumentException(nameof(yodaType));
            ShakespeakereType = !string.IsNullOrWhiteSpace(shakespeareType) ? shakespeareType : throw new ArgumentException(nameof(shakespeareType));
            YodaTranslationUrl = !string.IsNullOrWhiteSpace(yodaTranslationUrl) ? yodaTranslationUrl : throw new ArgumentException(nameof(yodaTranslationUrl));
            ShakeSpeareTranslationUrl = !string.IsNullOrWhiteSpace(shakespeareTranslationUrl) ? shakespeareTranslationUrl : throw new ArgumentException(nameof(shakespeareTranslationUrl));
        }

        public string Language { get; }
        public string CaveHabitat { get; }
        public string YodaType { get; }
        public string ShakespeakereType { get; }
        public string YodaTranslationUrl { get; }
        public string ShakeSpeareTranslationUrl { get; }
    }
}
