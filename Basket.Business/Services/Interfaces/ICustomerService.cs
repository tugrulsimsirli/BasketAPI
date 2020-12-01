using System.Threading.Tasks;
using Basket.Common.Models;

namespace Basket.Business.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<ResponseModel> GetCustomers();
        Task<ResponseModel> GetCustomers(int customerId);
        Task<ResponseModel> CustomerExist(int customerId);
    }
}