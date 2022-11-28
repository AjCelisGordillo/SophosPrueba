using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sophos.API.Models;

namespace Sophos.API.Core.IRepositories
{
    public interface ISaleRepository
    {
        IEnumerable<Sale> GetSales();
        Sale GetSaleById(int Id);
        bool CreateSale(Sale product);
        bool UpdateSale(Sale product);
        bool DeleteSale(int id);
    }
}
