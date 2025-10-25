using FakeStoreAPI.DTO;
using FakeStoreAPI.Model;

/*
 * Estas son las interfaces que utilizo para definir
 * los contratos que otras clases deben de cumplir
 */

namespace FakeStoreAPI.Interfaces
{
    public interface ICurrency
    {
        Task<List<Currency>> GetCurrenciesAsync();

        Task<Currency?> GetCurrencyByIdAsync(int id);

        Task<Currency?> GetCurrencyByNameAsync(string moneda);

        Task<Currency?> CreateCurrencyAsync(CurrencyDTO currency);

        Task<Currency?> UpdateCurrencyAsync(int id, Currency currency);

        Task<bool> DeleteCurrencyAsync(int id);
    }
}
