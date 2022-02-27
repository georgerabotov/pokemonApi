using PokeApiNet;

namespace PokeAPI.Domain.Models
{
    public class PokemonModel
    {
        public PokemonModel(string name, string description, string habitat, bool islegendary, List<PokemonSpeciesFlavorTexts> flavorTexts)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("please provide a name");
            Description = !string.IsNullOrWhiteSpace(description) ? description : throw new ArgumentException("please provide a description");
            Habitat = !string.IsNullOrWhiteSpace(habitat) ? habitat : "Unknown habitat";
            IsLegendary = islegendary;
            FlavorTextEntries = flavorTexts;
        }

        public void SetPokemonDescription(string description)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                Description = description;
            }
            else
            {
                throw new ArgumentException("Please provide description");
            }
        }

        public string Name { get; }
        public string Description { get; private set; }
        public string Habitat { get; }
        public bool IsLegendary { get; }
        public IReadOnlyList<PokemonSpeciesFlavorTexts> FlavorTextEntries { get; }
    }
}
