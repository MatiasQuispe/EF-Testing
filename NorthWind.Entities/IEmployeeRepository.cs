using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWind.DataAccess.Common;

namespace NorthWind.Entities
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        List<EmployeeInternal> GetAllInternalType();

        List<Employee> GetAllExternalType();
    }
}
