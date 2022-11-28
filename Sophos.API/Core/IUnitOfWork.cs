using Sophos.API.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sophos.API.Core
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IClientRepository Clients { get; }
        ISaleRepository Sales { get; }
    }
}
