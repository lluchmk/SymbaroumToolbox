namespace Abilities.Application.Options
{
    public class AuthOptions
    {
        public string Authority { get; set; }
        public string ApiName { get; set; }
        public string ApiSecret { get; set; }
        public bool RequiredHttpsMetadata { get; set; }
    }
}
