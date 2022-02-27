using AutoMapper;
using PokeApi.API.Responses;
using PokeAPI.Domain.Models;

namespace PokeApi.API.Mapping
{
    public class PokemonResponseMapping : Profile
    {
        public PokemonResponseMapping()
        {
            MapPokemonModelToResponse();
        }

        public void MapPokemonModelToResponse()
        {
            CreateMap<PokemonModel, PokemonResponse>()
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Habitat, src => src.MapFrom(x => x.Habitat))
                .ForMember(dest => dest.IsLegendary, src => src.MapFrom(x => x.IsLegendary))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description));
        }
    }
}
