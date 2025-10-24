using FakeStoreAPI.DTO;
using FakeStoreAPI.Interfaces;
using FakeStoreAPI.Model;
using FakeStoreAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencysController : ControllerBase
    {

        private readonly ICurrency _currencyService;

        public CurrencysController(ICurrency currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCurrencys()
        {
            var currencys = await _currencyService.GetCurrenciesAsync();
            return Ok(currencys);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrencyById(int id)
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }

        [HttpGet("ByName/{moneda}")]
        public async Task<IActionResult> GetCurrencyByName(string moneda)
        {
            var currency = await _currencyService.GetCurrencyByNameAsync(moneda);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrency([FromBody] CurrencyDTO currencydto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var currency = await _currencyService.CreateCurrencyAsync(currencydto);

            if (currency == null)
            {
                return BadRequest("Ya existe una moneda con ese nombre");
            }

            return Ok(currency);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrency(int id, [FromBody] Currency currency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updatedCurrency = await _currencyService.UpdateCurrencyAsync(id, currency);

            if (updatedCurrency == null)
            {
                return BadRequest("La moneda no existe");
            }

            return Ok(updatedCurrency);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            bool result = await _currencyService.DeleteCurrencyAsync(id);

            if (result == false)
            {
                return NotFound("No se encontro la moneda");
            }
            else
            {
                return Ok("Se elimino la moneda con el id" + id);
            }
        }
    }
}
