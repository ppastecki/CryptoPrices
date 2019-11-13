using System.Threading.Tasks;

namespace CryptoPrices.Service.Services
{
    public interface ICoinmarketClient
    {
        Task<string> GetLatestListings();
    }
}
