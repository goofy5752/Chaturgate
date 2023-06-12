namespace Chaturgate.Services.Interfaces
{
    public interface ICurrentUserService
    {
        string GetCurrentUserId();

        string GetCurrentUserEmail();

        string GetCurrentUserUserName();
    }
}
