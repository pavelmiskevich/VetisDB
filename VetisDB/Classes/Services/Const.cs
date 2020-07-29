using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace VetisDB.Classes.Services
{
    public static class Const
    {
        public static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
    }
}
