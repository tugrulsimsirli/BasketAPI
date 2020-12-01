using System;
using Basket.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Basket.Data.Domain;

namespace Basket.Business
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new BasketDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<BasketDbContext>>());
            context.Customers.AddRange(
                new Customer
                {
                    CustomerId = 1,
                    FirstName = "Tuğrul",
                    LastName = "Şimşirli"
                },
                new Customer
                {
                    CustomerId = 2,
                    FirstName = "Mahatma",
                    LastName = "Gandhi"
                });
            
            context.Products.AddRange(
                new Product
                {
                    Id = 1,
                    ProductName = "C#",
                    ProductStock = 100
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Python",
                    ProductStock = 0
                },
                new Product
                {
                    Id = 3,
                    ProductName = "Javascript",
                    ProductStock = 50
                });
            
            context.SaveChanges();
        }
    }
}