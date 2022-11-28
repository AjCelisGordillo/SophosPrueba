using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Sophos.API.Core.IRepositories;
using Sophos.API.Models;

namespace Sophos.API.Persistance.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DAL.DAL _DAL;

        public ClientRepository(DAL.DAL DAL)
        {
            _DAL = DAL;
        }
        public bool CreateClient(Client client)
        {
            var result = _DAL.SaveUpdateDelete(SPN.CreateClient, new object[]
            {
                client.DocumentId,
                client.Name,
                client.LastName,
                client.Telephone
            });

            return result;
        }

        public bool DeleteClient(int id)
        {
            var result = _DAL.SaveUpdateDelete(SPN.DeleteClient, new object[]
            {
                id
            });

            return result;
        }

        public Client GetClientById(int Id)
        {
            var client = new Client();
            DataTable result = _DAL.GetTable(SPN.GetClientById, new object[] { Id });

            if (result.Rows.Count == 0)
            {
                return null;
            }

            client.Id = (int)result.Rows[0]["Id"];
            client.DocumentId = (int)result.Rows[0]["DocumentId"];
            client.Name = result.Rows[0]["Name"].ToString();
            client.LastName = result.Rows[0]["LastName"].ToString();
            client.Telephone = (int)result.Rows[0]["Telephone"];
            return client;
        }

        public IEnumerable<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            DataTable result = _DAL.GetTable(SPN.GetClients);

            foreach (DataRow row in result.Rows)
            {
                var client = new Client();
                client.Id = (int)row["Id"];
                client.DocumentId = (int)row["DocumentId"];
                client.Name = row["Name"].ToString();
                client.LastName = row["LastName"].ToString();
                client.Telephone = (int)row["Telephone"];
                clients.Add(client);
            }
            return clients;
        }

        public bool UpdateClient(Client client)
        {
            var result = _DAL.SaveUpdateDelete(SPN.UpdateClient, new object[]
            {
               client.Id,
               client.DocumentId,
               client.Name,
               client.LastName,
               client.Telephone
            });
            return result;
        }
    }
}
