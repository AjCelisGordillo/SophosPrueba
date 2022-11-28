using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sophos.DAL
{
    class AppSettings
    {
        public static string DataSource = "(localdb)\\mssqllocaldb";
        public static string UserId = "sophosUser";
        public static string Password = "pass123*";
        public static string InitialCatalog = "SophosDb";
    }
}
