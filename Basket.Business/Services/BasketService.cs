using System;
using System.Linq;
using System.Threading.Tasks;
using Basket.Business.Services.Interfaces;
using Basket.Common.Models;
using Basket.Data;
using Basket.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Basket.Business.Services
{
    public class BasketService : IBasketService
    {
        private readonly ILogger _logger;
        private readonly BasketDbContext _db;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        public BasketService(ILogger logger, BasketDbContext db, ICustomerService customerService, IProductService productService)
        {
            _logger = logger;
            _db = db;
            _customerService = customerService;
            _productService = productService;
        }

        public async Task<ResponseModel> AddItem(int customerId, int productId, int amount)
        {
            try
            {
                var checkCustomer = await _customerService.CustomerExist(customerId);
                var checkProduct = await _productService.ProductExist(productId);
                if (checkCustomer.Data.Equals(false) || checkProduct.Data.Equals(false))
                {
                    return checkCustomer.Data.Equals(false) ? checkCustomer : checkProduct;
                }
                
                var customer = await _customerService.GetCustomers(customerId);
                var product = (Product)(await _productService.GetProducts(productId)).Data;

                var checkStock = CheckStock(product.ProductStock, amount);
                if (checkStock.Data.Equals(false))
                {
                    return checkStock;
                }

                if (await _db.Baskets.AnyAsync(b =>
                    b.Customer.CustomerId == customerId && b.BasketProducts.ProductId == productId))
                {
                    var updateResponse = await UpdateBasket(customerId, productId, amount);
                    return updateResponse;
                }

                await _db.Baskets.AnyAsync(b =>
                    b.Customer.CustomerId == customerId && b.BasketProducts.ProductId == productId);

                var basketProducts = new BasketProduct
                {
                    ProductId = product.Id, ProductName = product.ProductName, Amount = amount
                };

                var basket = new Data.Domain.Basket
                {
                    Customer = (Customer) customer.Data, BasketProducts = basketProducts
                };

                await _db.Baskets.AddAsync(basket);
                await _db.SaveChangesAsync();

                return new ResponseModel
                {
                    Message = "Başarılı",
                    Status = ResponseStatus.Success,
                    Data = basket
                };
            }
            catch (Exception e)
            {
                _logger.Error("AddItem Error: "+e.Message);
                return new ResponseModel
                {
                    Message = e.Message,
                    Status = ResponseStatus.Error,
                    Data = null
                };
            }
            
        }

        public async Task<ResponseModel> GetBasket(int customerId)
        {
            try
            {
                var data = await _db.Baskets.Where(b => b.Customer.CustomerId == customerId).Select(b => b.BasketProducts).ToListAsync();
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
                _logger.Error("GetBasket Error: "+e.Message);
                return new ResponseModel
                {
                    Message = e.Message,
                    Status = ResponseStatus.Error,
                    Data = null
                };
            }
        }
        
        private async Task<ResponseModel> UpdateBasket(int customerId, int productId, int amount)
        {
            // ReSharper disable once PossibleNullReferenceException
            var basketId = _db.Baskets.FirstOrDefault(item =>
                    item.Customer.CustomerId == customerId && item.BasketProducts.ProductId == productId)
                .BasketId;

            var entity = _db.BasketProducts.FirstOrDefault(e => e.BasketId == basketId);
            if (entity != null)
            {
                _db.BasketProducts.Attach(entity);
                entity.Amount = amount;
            }

            await _db.SaveChangesAsync();

            var currentBasket = _db.Baskets.FirstOrDefault(b => b.BasketId == basketId);
            return new ResponseModel
            {
                Message = "Ürünün sepet miktarı güncellendi",
                Status = ResponseStatus.Success,
                Data = currentBasket
            };
        }
        
        private static ResponseModel CheckStock(int productStock, int amount)
        {
            if (productStock < amount)
            {
                return new ResponseModel
                {
                    Message = "Ürünün yeterli stoğu bulunmamaktadır",
                    Status = ResponseStatus.Warning,
                    Data = false
                };
            }
            return new ResponseModel
            {
                Message = "Ürünün stoğu bulunmaktadır",
                Status = ResponseStatus.Warning,
                Data = true
            };
        }
    }
}