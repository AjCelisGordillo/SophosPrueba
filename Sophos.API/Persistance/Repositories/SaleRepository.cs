using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Sophos.API.Core.IRepositories;
using Sophos.API.Models;

namespace Sophos.API.Persistance.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DAL.DAL _DAL;

        public SaleRepository(DAL.DAL DAL)
        {
            _DAL = DAL;
        }
        public bool CreateSale(Sale sale)
        {
            var result = _DAL.SaveUpdateDelete(SPN.CreateSale, new object[]
            {
                sale.ClientId,
                sale.ProductId,
                sale.Quantity
            });

            return result;
        }

        public bool DeleteSale(int id)
        {
            var result = _DAL.SaveUpdateDelete(SPN.DeleteSale, new object[]
            {
                id
            });

            return result;
        }

        public Sale GetSaleById(int Id)
        {
            var sale = new Sale();
            sale.Client = new Client();
            sale.Product = new Product();

            DataTable result = _DAL.GetTable(SPN.GetSaleById, new object[] { Id });

            if (result.Rows.Count == 0)
            {
                return null;
            }

            sale.SaleId = (int) result.Rows[0]["SaleId"];
            sale.ClientId = (int) result.Rows[0]["ClientId"];
            sale.ProductId = (int)result.Rows[0]["ProductId"];
            sale.Quantity = (int)result.Rows[0]["Quantity"];
            sale.PricePerUnit = (decimal)result.Rows[0]["PricePerUnit"];
            sale.Total = (decimal)result.Rows[0]["Total"];
            sale.Client.Id = (int) result.Rows[0]["Id"];
            sale.Client.DocumentId = (int) result.Rows[0]["DocumentId"];
            sale.Client.Name = result.Rows[0]["Name"].ToString();
            sale.Client.LastName = result.Rows[0]["LastName"].ToString();
            sale.Product.ProductId = (int) result.Rows[0]["ProductId"];
            sale.Product.Name = result.Rows[0]["ProductName"].ToString();
            sale.Product.Price = (decimal) result.Rows[0]["Price"];
            return sale;
        }

        public IEnumerable<Sale> GetSales()
        {
            List<Sale> sales = new List<Sale>();
            DataTable result = _DAL.GetTable(SPN.GetSales);

            foreach (DataRow row in result.Rows)
            {
                var sale = new Sale();
                sale.Client = new Client();
                sale.Product = new Product();
                sale.SaleId = (int)row["SaleId"];
                sale.ClientId = (int) row["ClientId"];
                sale.ProductId = (int)row["ProductId"];
                sale.Quantity = (int)row["Quantity"];
                sale.PricePerUnit = (decimal)row["PricePerUnit"];
                sale.Total = (decimal) row["Total"];
                sale.Client.Id = (int) row["Id"];
                sale.Client.DocumentId = (int) row["DocumentId"];
                sale.Client.Name = row["Name"].ToString();
                sale.Client.LastName = row["LastName"].ToString();
                sale.Product.ProductId = (int) row["ProductId"];
                sale.Product.Name = row["ProductName"].ToString();
                sale.Product.Price = (decimal)row["Price"];
                sales.Add(sale);
            }
            return sales;
        }

        public bool UpdateSale(Sale sale)
        {
            var result = _DAL.SaveUpdateDelete(SPN.UpdateSale, new object[]
            {
                sale.SaleId,
                sale.ClientId,
                sale.ProductId,
                sale.Quantity
            });
            return result;
        }
    }
}
