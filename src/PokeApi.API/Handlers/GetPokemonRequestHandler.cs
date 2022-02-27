using AutoMapper;
using MediatR;
using PokeApi.API.Requests;
using PokeApi.API.Responses;
using PokeAPI.Domain;

namespace PokeApi.API.Handlers
{
    public class GetPokemonRequestHandler : IRequestHandler<GetPokemonRequest, PokemonResponse>
    {
        private readonly IGetPokemonData _pokemonData;
        private readonly IMapper _mapper;

        public GetPokemonRequestHandler(IGetPokemonData pokemonData, IMapper mapper)
        {
            _pokemonData = pokemonData;
            _mapper = mapper;
        }
        public async Task<PokemonResponse> Handle(GetPokemonRequest request, CancellationToken cancellationToken)
        {
            var pokemon = await _pokemonData.GetPokemonAsync(request.PokemonName);
            return _mapper.Map<PokemonResponse>(pokemon);
        }
    }
}
