using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class SaleVM
    {
        public int SaleId { get; set; }
        [DisplayName("Cliente")]
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        [Required]
        [DisplayName("Cantidad")]
        public int Quantity { get; set; }
        [DisplayName("Precio Unitario")]
        public decimal PricePerUnit { get; set; }
        public decimal Total { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
    }
}
