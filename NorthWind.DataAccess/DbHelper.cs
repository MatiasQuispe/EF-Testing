using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DataAccess
{
    public static class DbHelper
    {
        public static void CreateDb()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<NorthWindContext>());
        }
    }
}
