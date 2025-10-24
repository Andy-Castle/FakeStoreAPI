using FakeStoreAPI.Data;
using FakeStoreAPI.DTO;
using FakeStoreAPI.Interfaces;
using FakeStoreAPI.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FakeStoreAPI.Services
{
    public class CurrencyServices : ICurrency
    {
        private readonly ApplicationDbContext _context;


        public CurrencyServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Currency>> GetCurrenciesAsync()
        {
            try
            {
                return await _context.Currencys.ToListAsync();

            }catch( Exception ex)
            {
                throw new Exception($"No se pueden obtener las monedas" + ex.Message);
            }
        }

        public async Task<Currency?> GetCurrencyByIdAsync(int id)
        {
            try
            {
                var currency = await _context.Currencys.FindAsync(id);

                return currency;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se puedo obtener la moneda mediante su id" + ex.Message);
            }
        }

        public async Task<Currency?> GetCurrencyByNameAsync(string moneda)
        {
            try
            {
                var currency = await _context.Currencys.FirstOrDefaultAsync(c => c.Moneda == moneda);

                return currency;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se puedo obtener la moneda mediante su nombre" + ex.Message);
            }
        }

        public async Task<Currency?> CreateCurrencyAsync(CurrencyDTO currency)
        {
            try
            {
                var existingCurrency = await _context.Currencys
                .FirstOrDefaultAsync(c => c.Moneda == currency.Moneda);

                if (existingCurrency != null)
                {
                    return null;
                }

                var newCurrency = new Currency
                {
                    Moneda = currency.Moneda,
                    Equivalente = currency.Equivalente
                };

                _context.Currencys.Add(newCurrency);
                await _context.SaveChangesAsync();

                return newCurrency;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se puedo crear la moneda" + ex.Message);
            }
        }
        public async Task<Currency?> UpdateCurrencyAsync(int id, Currency currency)
        {
            try
            {

                var existingCurrency = await _context.Currencys.FindAsync(id);
                if (existingCurrency == null)
                {
                    return null;
                }

                existingCurrency.Moneda = currency.Moneda;
                existingCurrency.Equivalente = currency.Equivalente;
                _context.Currencys.Update(existingCurrency);
                await _context.SaveChangesAsync();
                return existingCurrency;


            }
            catch(Exception ex)
            {
                throw new Exception($"No se puedo actualizar la moneda " + ex.Message);

            }
        }

        public async Task<bool> DeleteCurrencyAsync(int id)
        {
            try
            {

                var existingCurrency = await _context.Currencys.FindAsync(id);
                if (existingCurrency == null)
                {
                    return false;
                }

                _context.Currencys.Remove(existingCurrency);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"No se puedo eliminar la moneda " + ex.Message);

            }

        }


    }

}
