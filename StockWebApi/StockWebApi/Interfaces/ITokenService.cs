using StockWebApi.Model;

namespace StockWebApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
