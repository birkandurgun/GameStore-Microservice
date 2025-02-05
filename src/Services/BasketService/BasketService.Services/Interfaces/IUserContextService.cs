namespace BasketService.Services.Interfaces
{
    public interface IUserContextService
    {
        string GetUsername();
        string GetEmail();
        Guid GetUserId();
        List<string> GetRoles();
    }
}
