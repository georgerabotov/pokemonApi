using PokeAPI.Domain.Models;
using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeApi.Persistence.Helpers
{
    internal static class TranslatorHelper
    {
        public static int RandomTranslationSelector(this PokemonModel species, string language)
        {
            var rnd = new Random();
            var flavorTextEntriesInEnglish = new List<PokemonSpeciesFlavorTexts>();
            flavorTextEntriesInEnglish = species.FlavorTextEntries.Where(x => x.Language.Name == language).ToList();
            return rnd.Next(flavorTextEntriesInEnglish.Count());
        }
    }
}
