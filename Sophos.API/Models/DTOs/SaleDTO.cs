using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sophos.API.Models.DTOs
{
    public class SaleDTO
    {
        public int SaleId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Total { get; set; }

        public Client Client { get; set; }
        public Product Product { get; set; }
    }
}
