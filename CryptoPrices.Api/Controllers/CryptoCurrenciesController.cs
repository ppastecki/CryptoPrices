using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CryptoPrices.Core.Data;
using CryptoPrices.Core.Entities;

namespace CryptoPrices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoCurrenciesController : ControllerBase
    {
        private readonly CryptoPricesContext _context;

        public CryptoCurrenciesController(CryptoPricesContext context)
        {
            _context = context;
        }

        // GET: api/CryptoCurrencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CryptoCurrency>>> GetCryptoCurrencies()
        {
            return await _context.CryptoCurrencies.ToListAsync();
        }

        // GET: api/CryptoCurrencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CryptoCurrency>> GetCryptoCurrency(int id)
        {
            var cryptoCurrency = await _context.CryptoCurrencies.FindAsync(id);

            if (cryptoCurrency == null)
            {
                return NotFound();
            }

            return cryptoCurrency;
        }

        // PUT: api/CryptoCurrencies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCryptoCurrency(int id, CryptoCurrency cryptoCurrency)
        {
            if (id != cryptoCurrency.Id)
            {
                return BadRequest();
            }

            _context.Entry(cryptoCurrency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CryptoCurrencyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CryptoCurrencies
        [HttpPost]
        public async Task<ActionResult<CryptoCurrency>> PostCryptoCurrency(CryptoCurrency cryptoCurrency)
        {
            _context.CryptoCurrencies.Add(cryptoCurrency);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CryptoCurrencyExists(cryptoCurrency.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCryptoCurrency", new { id = cryptoCurrency.Id }, cryptoCurrency);
        }

        // DELETE: api/CryptoCurrencies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CryptoCurrency>> DeleteCryptoCurrency(int id)
        {
            var cryptoCurrency = await _context.CryptoCurrencies.FindAsync(id);
            if (cryptoCurrency == null)
            {
                return NotFound();
            }

            _context.CryptoCurrencies.Remove(cryptoCurrency);
            await _context.SaveChangesAsync();

            return cryptoCurrency;
        }

        private bool CryptoCurrencyExists(int id)
        {
            return _context.CryptoCurrencies.Any(e => e.Id == id);
        }
    }
}
