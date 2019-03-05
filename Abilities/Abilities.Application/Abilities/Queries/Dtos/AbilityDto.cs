using Abilities.Domain.Enums;

namespace Abilities.Application.Abilities.Queries.Dtos
{
    public class AbilityDto : BaseAbilityDto
    {
        public ActionType NoviceType { get; set; }
        public string NoviceDescription { get; set; }

        public ActionType AdeptType { get; set; }
        public string AdeptDescription { get; set; }

        public ActionType MasterType { get; set; }
        public string MasterDescription { get; set; }
    }
}
