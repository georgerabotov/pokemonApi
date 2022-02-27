using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokeApi.API.Requests;
using PokeApi.Core;

namespace PokeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ApiControllerBase
    {
        public PokemonController(IMediator mediator) : base(mediator)
        {

        }

        [HttpGet("{pokemonname}")]
        public async Task<IActionResult> Get(string pokemonname)
        {
            return await Ok(new GetPokemonRequest(pokemonname));
        }

        [HttpGet("pokemon/translated/{pokemonname}")]
        public async Task<IActionResult> GetTranslated(string pokemonname)
        {
            return await Ok(new GetTranslatedPokemonRequest(pokemonname));
        }
    }
}
