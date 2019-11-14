using CryptoPrices.Core.Repositories;
using CryptoPrices.Web.ModelFactories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CryptoPrices.Web.Controllers
{
    public class CryptoCurrencyController : Controller
    {
        private readonly ICryptoCurrencyModelFactory _cryptoCurrencyModelFactory;
        private readonly ICryptocurrencyRepository _cryptocurrencyRepository;

        public CryptoCurrencyController(ICryptoCurrencyModelFactory cryptoCurrencyModelFactory, ICryptocurrencyRepository cryptocurrencyRepository)
        {
            _cryptoCurrencyModelFactory = cryptoCurrencyModelFactory;
            _cryptocurrencyRepository = cryptocurrencyRepository;
        }

        public async Task<IActionResult> Index()
        {
            var currencies = await _cryptocurrencyRepository.GetAllAsync();
            var model = _cryptoCurrencyModelFactory.GetIndexModel(currencies);

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var currency = await _cryptocurrencyRepository.GetAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            var model = _cryptoCurrencyModelFactory.GetDetailsModel(currency);

            return View(model);
        }
    }
}
