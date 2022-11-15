using NorthWind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.DataAccess
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Retorna todos los empleados externos a la empresa
        /// </summary>
        /// <returns></returns>
        public List<EmployeeInternal> GetAllInternalType()
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                return context.Employees.OfType<EmployeeInternal>().ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAllExternalType()
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                var result = from employee in context.Employees
                             where employee is EmployeeExternal
                             select employee;

                return result.ToList();
            }
        }

    }

    public class EmployeeInternalRepository : BaseRepository<EmployeeInternal>
    {

    }

    public class EmployeeExternalRepository : BaseRepository<EmployeeExternal>
    {

    }

}
