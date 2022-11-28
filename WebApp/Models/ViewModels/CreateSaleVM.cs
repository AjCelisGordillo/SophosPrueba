using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class CreateSaleVM
    {
        [DisplayName("Cliente")]
        public int ClientId { get; set; }
        [DisplayName("Producto")]
        public int ProductId { get; set; }
        [DisplayName("Cantidad")]
        public int Quantity { get; set; }
        
    }
}
