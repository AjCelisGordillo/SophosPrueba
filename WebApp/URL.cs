using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class URL
    {
        public static string baseUrl = "https://localhost:44397/";
        public static string GetProducts = baseUrl + "api/product/";
        public static string GetProductById = baseUrl + "api/product/";
        public static string UpdateProduct = baseUrl + "api/product/";
        public static string CreateProduct = baseUrl + "api/product";
        public static string DeleteProduct = baseUrl + "api/product/";

        public static string GetClients = baseUrl + "api/client/";
        public static string GetClientById = baseUrl + "api/client/";
        public static string UpdateClient = baseUrl + "api/client/";
        public static string CreateClient = baseUrl + "api/client";
        public static string DeleteClient = baseUrl + "api/client/";

        public static string GetSales = baseUrl + "api/sale/";
        public static string GetSaleById = baseUrl + "api/sale/";
        public static string UpdateSale = baseUrl + "api/sale/";
        public static string CreateSale = baseUrl + "api/sale";
        public static string DeleteSale = baseUrl + "api/sale/";
    }
}
