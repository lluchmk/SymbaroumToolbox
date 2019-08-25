namespace Abilities.Application.Interfaces.Services
{
    public interface IUsersService
    {
        bool IsUserAuthenticated();
        string GetUserId();
    }
}