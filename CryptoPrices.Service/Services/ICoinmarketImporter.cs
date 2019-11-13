using System.Threading.Tasks;

namespace CryptoPrices.Service.Services
{
    public interface ICoinmarketImporter
    {
        Task Import();
    }
}
