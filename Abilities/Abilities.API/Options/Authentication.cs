namespace Abilities.API.Options
{
    public class Authentication
    {
        public string Authority { get; set; }
        public string ApiName { get; set; }
        public bool RequiredHttpsMetadata { get; set; }
    }
}
