using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sophos.API.Models
{
    public class Client
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Telephone { get; set; }
    }
}
