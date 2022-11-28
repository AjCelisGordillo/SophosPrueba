using Sophos.API.Core;
using Sophos.API.Core.IRepositories;
using Sophos.API.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sophos.API.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DAL.DAL _DAL;
        public IProductRepository Products { get; private set; }
        public IClientRepository Clients { get; private set; }

        public ISaleRepository Sales { get; private set; }

        public UnitOfWork()
        {
            _DAL = new DAL.DAL();
            Products = new ProductRepository(_DAL);
            Clients = new ClientRepository(_DAL);
            Sales = new SaleRepository(_DAL);
        }
    }
}
