namespace BBQ.DataAccess.Services
{
    public interface IClaimService
    {
        string GetUserId();

        string GetClaim(string key);
    }
}
