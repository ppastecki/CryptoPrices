using System.Net.Mime;
using System.ComponentModel.DataAnnotations;
using CryptoPrices.Core.ModelFactories;
using CryptoPrices.Core.Models;
using CryptoPrices.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<IEnumerable<CryptoCurrency>>> GetCryptoCurrencies()
        {
            var currencies = await _cryptocurrencyRepository.GetAllAsync();
            var model = _cryptoCurrencyModelFactory.GetIndexModel(currencies);

            return new JsonResult(model);
        }

        // GET: api/CryptoCurrency/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CryptoCurrencyDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CryptoCurrencyDetails>> GetCryptoCurrency(int id)
        {
            var currency = await _cryptocurrencyRepository.GetAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            var model = _cryptoCurrencyModelFactory.GetDetailsModel(currency);

            return new JsonResult(model);
        }
    }
}
