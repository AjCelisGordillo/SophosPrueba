using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Total { get; set; }
    }
}
