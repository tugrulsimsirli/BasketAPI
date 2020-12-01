using System;
using System.Threading.Tasks;
using Basket.Business.Services.Interfaces;
using Basket.Common.Models;
using Basket.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Basket.Business.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ILogger _logger;
        private readonly BasketDbContext _db;
        public CustomerService(ILogger logger, BasketDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public async Task<ResponseModel> GetCustomers()
        {
            try
            {
                var data = await _db.Customers.ToListAsync();
                var response = new ResponseModel
                {
                    Message = "Başarılı",
                    Status = ResponseStatus.Success,
                    Data = data
                };
                return response;
            }
            catch (Exception e)
            {
                var response = new ResponseModel
                {
                    Message = e.Message,
                    Status = ResponseStatus.Error,
                    Data = null
                };
                _logger.Error("GetConsumers Error: "+e.Message);
                return response;
            }
        }
        
        public async Task<ResponseModel> GetCustomers(int customerId)
        {
            try
            {
                var data = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
                var response = new ResponseModel
                {
                    Message = "Başarılı",
                    Status = ResponseStatus.Success,
                    Data = data
                };
                return response;
            }
            catch (Exception e)
            {
                var response = new ResponseModel
                {
                    Message = e.Message,
                    Status = ResponseStatus.Error,
                    Data = null
                };
                _logger.Error("GetConsumers Error: "+e.Message);
                return response;
            }
        }
        
        public async Task<ResponseModel> CustomerExist(int customerId)
        {
            try
            {
                var data = await _db.Customers.AnyAsync(c => c.CustomerId == customerId);
                var msg = data ? "Kullanıcı tanımlı" : "Böyle bir kullanıcı yok";
                var response = new ResponseModel
                {
                    Message = msg,
                    Status = ResponseStatus.Success,
                    Data = data
                };
                return response;
            }
            catch (Exception e)
            {
                var response = new ResponseModel
                {
                    Message = e.Message,
                    Status = ResponseStatus.Error,
                    Data = null
                };
                _logger.Error("GetConsumers Error: "+e.Message);
                return response;
            }
        }
    }
}