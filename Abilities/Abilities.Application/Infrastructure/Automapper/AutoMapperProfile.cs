using Abilities.Application.Abilities.Queries.Dtos;
using Abilities.Domain.Entities;
using AutoMapper;

namespace Abilities.Application.Infrastructure.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BaseAbility, BaseAbilityDto>();

            CreateMap<Ability, AbilityDto>()
                //.Include<BaseAbility, BaseAbilityDto>()
                .ForMember(d => d.NoviceDescription, opt => opt.MapFrom(s => s.Novice.Description))
                .ForMember(d => d.NoviceType, opt => opt.MapFrom(s => s.Novice.Type))
                .ForMember(d => d.AdeptDescription, opt => opt.MapFrom(s => s.Adept.Description))
                .ForMember(d => d.AdeptType, opt => opt.MapFrom(s => s.Adept.Type))
                .ForMember(d => d.MasterDescription, opt => opt.MapFrom(s => s.Master.Description))
                .ForMember(d => d.MasterType, opt => opt.MapFrom(s => s.Master.Type))
                ;

            CreateMap<MysticalPower, MysticalPowerDto>();

            CreateMap<Ritual, RitualDto>();
        }
    }
}
