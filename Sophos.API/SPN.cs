using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sophos.API
{
    public class SPN
    {
        public static string GetProducts = "dbo.GetProducts";
        public static string GetProductById = "dbo.GetProductById";
        public static string UpdateProduct = "dbo.UpdateProduct";
        public static string CreateProduct = "dbo.CreateProduct";
        public static string DeleteProduct = "dbo.EliminateProduct";

        public static string GetClients = "dbo.GetClients";
        public static string GetClientById = "dbo.GetClientById";
        public static string UpdateClient= "dbo.ModifyClient";
        public static string CreateClient= "dbo.CreateClient";
        public static string DeleteClient= "dbo.EliminateClient";

        public static string GetSales = "dbo.GetSales";
        public static string GetSaleById = "dbo.GetSaleById";
        public static string UpdateSale = "dbo.ModifySale";
        public static string CreateSale = "dbo.CreateSale";
        public static string DeleteSale = "dbo.DeleteSale";
    }                              
}
