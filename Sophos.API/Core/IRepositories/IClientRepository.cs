using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sophos.API.Models;

namespace Sophos.API.Core.IRepositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
        Client GetClientById(int Id);
        bool CreateClient(Client client);
        bool UpdateClient(Client client);
        bool DeleteClient(int id);
    }
}
