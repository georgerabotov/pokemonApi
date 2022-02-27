using PokeAPI.Domain.Models;
using PokeApiNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeAPI.Domain
{
    public interface IGetPokemonData
    {
        Task<PokemonModel> GetPokemonAsync(string pokemonName);
    }
}
