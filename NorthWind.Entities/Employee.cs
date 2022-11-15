using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Entities
{
    public abstract class Employee
    {
        public int EmployeeID { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

    }

    public class EmployeeExternal : Employee
    {
        public string ConsultantName { get; set; }

        public DateTime ContactExpiration { get; set; }
    }

    public class EmployeeInternal : Employee
    {
        public DateTime HireDate { get; set; }
    }

}
