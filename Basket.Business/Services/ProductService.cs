using System;
using System.Threading.Tasks;
using Basket.Business.Services.Interfaces;
using Basket.Common.Models;
using Basket.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Basket.Business.Services
{
    public class ProductService: IProductService
    {
        private readonly ILogger _logger;
        private readonly BasketDbContext _db;
        public ProductService(ILogger logger, BasketDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<ResponseModel> GetProducts()
        {
            try
            {
                var data = await _db.Products.ToListAsync();
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
                _logger.Error("GetProducts Error: "+e.Message);
                return response;
            }
        }
        public async Task<ResponseModel> GetProducts(int productId)
        {
            try
            {
                var data = await _db.Products.FirstOrDefaultAsync(p => p.Id == productId);
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
                _logger.Error("GetProducts Error: "+e.Message);
                return response;
            }
        }
        
        public async Task<ResponseModel> ProductExist(int productId)
        {
            try
            {
                var data = await _db.Products.AnyAsync(p => p.Id == productId);
                var msg = data ? "Ürün sisteme kayıtlı." : "Böyle bir ürün bulunmamaktadır.";
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
                _logger.Error("GetProducts Error: "+e.Message);
                return response;
            }
        }
    }
}