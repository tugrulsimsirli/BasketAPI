using System.Threading.Tasks;
using Basket.Common.Models;

namespace Basket.Business.Services.Interfaces
{
    public interface IBasketService
    {
        Task<ResponseModel> AddItem(int customerId, int productId, int amount);
        Task<ResponseModel> GetBasket(int customerId);
    }
}