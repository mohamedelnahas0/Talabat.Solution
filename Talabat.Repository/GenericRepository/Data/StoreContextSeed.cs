using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Entites.OrderAggregate;

namespace Talabat.Repository.Generic_Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbContext)
        {
            if (_dbContext.ProductBrands.Count() ==0)
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Generic_Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count() > 0)
                {


                    foreach (var brand in brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbContext.SaveChangesAsync();
                }

            }
            if (_dbContext.ProductCategories.Count() == 0)
            {
                var CategoriesData = File.ReadAllText("../Talabat.Repository/Generic_Repository/Data/DataSeed/Categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);
                if (Categories?.Count() > 0)
                {


                    foreach (var category in Categories)
                    {
                        _dbContext.Set<ProductCategory>().Add(category);
                    }
                    await _dbContext.SaveChangesAsync();
                }

            }
            if (_dbContext.Products.Count() == 0)
            {
                var productsData = File.ReadAllText("../Talabat.Repository/Generic_Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count() > 0)
                {


                    foreach (var product in products)
                    {
                        _dbContext.Set<Product>().Add(product);
                    }
                    await _dbContext.SaveChangesAsync();
                }

            }

            if (_dbContext.DeliveryMethods.Count() == 0)
            {
                var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/GenericRepository/Data/DataSeed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                if (DeliveryMethods?.Count > 0)
                {


                    foreach (var DeliveryMethod in DeliveryMethods)
                    {
                        _dbContext.Set<DeliveryMethod>().Add(DeliveryMethod);
                    }
                    await _dbContext.SaveChangesAsync();
                }

            }

        }
    }
}
