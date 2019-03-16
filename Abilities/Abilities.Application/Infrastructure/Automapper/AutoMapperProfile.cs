using Abilities.Application.Abilities.Commands.CreateAbility;
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

            // TODO: Tiered skill dto and correct mapping
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

            CreateMap<CreateSkillCommand, BaseAbility>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Description))
                ;

            CreateMap<CreateSkillCommand, TieredAbility>()
                .IncludeBase<CreateSkillCommand, BaseAbility>()
                .ForMember(d => d.Novice, opt => opt.MapFrom(s => new AbilityTier(s.NoviceType, s.NoviceDescription)))
                .ForMember(d => d.Adept, opt => opt.MapFrom(s => new AbilityTier(s.AdeptType, s.AdeptDescription)))
                .ForMember(d => d.Master, opt => opt.MapFrom(s => new AbilityTier(s.MasterType, s.MasterDescription)))
                ;

            CreateMap<CreateSkillCommand, Ability>()
                .IncludeBase<CreateSkillCommand, TieredAbility>()
                ;

            CreateMap<CreateSkillCommand, Ritual>()
                .ForMember(d => d.Tradition, opt => opt.MapFrom(s => s.Tradition))
                ;

            CreateMap<CreateSkillCommand, MysticalPower>()
                .IncludeBase<CreateSkillCommand, TieredAbility>()
                .ForMember(d => d.Material, opt => opt.MapFrom(s => s.Material))
                ;
        }
    }
}
