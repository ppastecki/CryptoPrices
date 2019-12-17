using CryptoPrices.Core.ModelFactories;
using CryptoPrices.Core.Models;
using CryptoPrices.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoPrices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoCurrencyController : ControllerBase
    {
        private readonly ICryptoCurrencyModelFactory _cryptoCurrencyModelFactory;
        private readonly ICryptocurrencyRepository _cryptocurrencyRepository;

        public CryptoCurrencyController(ICryptoCurrencyModelFactory cryptoCurrencyModelFactory, ICryptocurrencyRepository cryptocurrencyRepository)
        {
            _cryptoCurrencyModelFactory = cryptoCurrencyModelFactory;
            _cryptocurrencyRepository = cryptocurrencyRepository;
        }

        // GET: api/CryptoCurrency
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CryptoCurrency>>> GetCryptoCurrencies()
        {
            var currencies = await _cryptocurrencyRepository.GetAllAsync();
            var model = _cryptoCurrencyModelFactory.GetIndexModel(currencies);

            return Ok(model);
        }

        // GET: api/CryptoCurrency/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CryptoCurrency>> GetCryptoCurrency(int id)
        {
            var currency = await _cryptocurrencyRepository.GetAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            var model = _cryptoCurrencyModelFactory.GetDetailsModel(currency);

            return Ok(model);
        }
    }
}
