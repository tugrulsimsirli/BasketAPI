using System.Threading.Tasks;
using Basket.Common.Models;

namespace Basket.Business.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResponseModel> GetProducts();
        Task<ResponseModel> GetProducts(int productId);
        Task<ResponseModel> ProductExist(int productId);
    }
}