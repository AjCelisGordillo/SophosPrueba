using Sophos.API.Core.IRepositories;
using Sophos.API.Models;
using Sophos.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Sophos.API.Persistance.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DAL.DAL _DAL;

        public ProductRepository(DAL.DAL DAL)
        {
            _DAL = DAL;
        }

        public bool CreateProduct(Product product)
        {
            var result = _DAL.SaveUpdateDelete(SPN.CreateProduct, new object[]
            {
                product.Name,
                product.Price
            });

            return result;
        }

        public bool DeleteProduct(int id)
        {
            var result = _DAL.SaveUpdateDelete(SPN.DeleteProduct, new object[]
            {
                id
            });

            return result;
        }

        public Product GetProductById(int Id)
        {
            var product = new Product();
            DataTable result = _DAL.GetTable(SPN.GetProductById, new object[] {Id});

            if (result.Rows.Count == 0)
            {
                return null;
            }

            product.ProductId = (int)result.Rows[0]["ProductId"];
            product.Name = result.Rows[0]["Name"].ToString();
            product.Price = (decimal)result.Rows[0]["Price"];
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            DataTable result = _DAL.GetTable(SPN.GetProducts);

            foreach(DataRow row in result.Rows)
            {
                var product = new Product();
                product.ProductId = (int)row["ProductId"];
                product.Name = row["Name"].ToString();
                product.Price = (decimal)row["Price"];
                products.Add(product);
            }
            return products;
        }

        public bool UpdateProduct(Product product)
        {
            var result = _DAL.SaveUpdateDelete(SPN.UpdateProduct, new object[]
            {
                product.ProductId,
                product.Name,
                product.Price
            });
            return result;
        }
    }
}
