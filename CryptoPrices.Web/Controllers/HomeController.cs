using CryptoPrices.Core.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CryptoPrices.Web.Models;

namespace CryptoPrices.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CryptoPricesContext _context;

        public HomeController(CryptoPricesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var currencies = _context.CryptoCurrencies.ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
