using AutoMapper;
using MediatR;
using PokeApi.API.Requests;
using PokeApi.API.Responses;
using PokeAPI.Domain;

namespace PokeApi.API.Handlers
{
    public class GetTranslatedPokemonRequestHandler : IRequestHandler<GetTranslatedPokemonRequest, PokemonResponse>
    {
        private readonly IGetPokemonData _pokemonData;
        private readonly ITranslateDescription _translator;
        private readonly IMapper _mapper;
        public GetTranslatedPokemonRequestHandler(IGetPokemonData pokemonData, ITranslateDescription translator, IMapper mapper)
        {
            _pokemonData = pokemonData;
            _translator = translator;
            _mapper = mapper;
        }
        public async Task<PokemonResponse> Handle(GetTranslatedPokemonRequest request, CancellationToken cancellationToken)
        {
            var pokemon = await _pokemonData.GetPokemonAsync(request.PokemonName);
            pokemon.SetPokemonDescription(await _translator.TranslatePokemonDescription(pokemon));
            return _mapper.Map<PokemonResponse>(pokemon);
        }
    }
}
