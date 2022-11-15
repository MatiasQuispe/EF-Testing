using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWind.DataAccess;
using NorthWind.Entities;
using System.Collections.Generic;
using System.Linq;
using NorthWind.DataAccess.Common;

namespace EF.Test
{

    [TestClass]
    public class EmployeeTest
    {
        private EmployeeInternal employee1 = null;
        private EmployeeExternal employee2 = null;
        private EmployeeExternal employee3 = null;

        [TestInitialize]
        public void inicializar()
        {
            DbHelper.CreateDb();
        }


        private void InitializeTestData()
        {
            IEmployeeRepository repoEmployee = new EmployeeRepository();
            
            //
            // elimino registros previos
            //
            List<Employee> list = repoEmployee.GetAll();

            list.ForEach(x => repoEmployee.Delete(x));


            //
            // creo un empleado interno
            //
            employee1 = new EmployeeInternal()
            {
                FirstName = "name1",
                LastName = "lastname1",
                HireDate = DateTime.Now.AddMonths(-10)
            };
            repoEmployee.Create(employee1);

            //
            // creo un empleado externo
            //
            employee2 = new EmployeeExternal()
            {
                FirstName = "name2",
                LastName = "lastname2",
                ConsultantName = "ConsultantName2",
                ContactExpiration = DateTime.Now.AddYears(2)
            };
            repoEmployee.Create(employee2);

            //
            // creo otro empleado externo
            //
            employee3 = new EmployeeExternal()
            {
                FirstName = "name3",
                LastName = "lastname3",
                ConsultantName = "ConsultantName3",
                ContactExpiration = DateTime.Now.AddYears(1)
            };
            repoEmployee.Create(employee3);
        }


        [TestMethod]
        public void GetAll_Employee()
        {
            InitializeTestData();

            //
            //recupero todos los empleados
            //
            IEmployeeRepository repoEmployee = new EmployeeRepository();

            List<Employee> listIntEmployee = repoEmployee.GetAll()
                                                            .OrderBy(x=>x.EmployeeID)
                                                            .ToList();

            //
            // Assert
            //
            Assert.AreEqual(listIntEmployee.Count, 3);

            Assert.IsInstanceOfType(listIntEmployee[0], typeof(EmployeeInternal));
            Assert.IsInstanceOfType(listIntEmployee[1], typeof(EmployeeExternal));
            Assert.IsInstanceOfType(listIntEmployee[2], typeof(EmployeeExternal));

        }

        [TestMethod]
        public void GetAllInternal_UsingSpecificRepository_Employee()
        {
            InitializeTestData();

            //
            //recupero solo empleados internos
            //
            IRepository<EmployeeInternal> repoInternalEmployee = new EmployeeInternalRepository();

            List<EmployeeInternal> listIntEmployee = repoInternalEmployee.GetAll();

            //
            // Assert
            //
            Assert.AreEqual(listIntEmployee.Count, 1);

            Assert.IsInstanceOfType(listIntEmployee[0], typeof(EmployeeInternal));

            Assert.AreEqual(listIntEmployee[0].FirstName, employee1.FirstName);
            Assert.AreEqual(listIntEmployee[0].HireDate.ToShortDateString(), employee1.HireDate.ToShortDateString());

        }

        [TestMethod]
        public void GetAllInternal_UsingGenericRepository_Employee()
        {

            InitializeTestData();

            //
            //recupero solo empleados internos
            //
            IEmployeeRepository repoEmployee = new EmployeeRepository();

            List<EmployeeInternal> listIntEmployee = repoEmployee.GetAllInternalType();

            //
            // Assert
            //
            Assert.AreEqual(listIntEmployee.Count, 1);

            Assert.IsInstanceOfType(listIntEmployee[0], typeof(EmployeeInternal));

            Assert.AreEqual(listIntEmployee[0].FirstName, employee1.FirstName);
            Assert.AreEqual(listIntEmployee[0].HireDate.ToShortDateString(), employee1.HireDate.ToShortDateString());

        }


        [TestMethod]
        public void GetAllExternal_Employee()
        {

            InitializeTestData();

            //
            //recupero solo empleados externos
            //
            IRepository<EmployeeExternal> repoExternalEmployee = new EmployeeExternalRepository();

            List<EmployeeExternal> listExtEmployee = repoExternalEmployee.GetAll();

            //
            // Assert
            //
            Assert.AreEqual(listExtEmployee.Count, 2);

            Assert.IsInstanceOfType(listExtEmployee[0], typeof(EmployeeExternal));
            Assert.IsInstanceOfType(listExtEmployee[1], typeof(EmployeeExternal));

            Assert.AreEqual(listExtEmployee[0].FirstName, employee2.FirstName);
            Assert.AreEqual(listExtEmployee[0].ContactExpiration.ToShortDateString(), employee2.ContactExpiration.ToShortDateString());
            Assert.AreEqual(listExtEmployee[0].ConsultantName, employee2.ConsultantName);

            Assert.AreEqual(listExtEmployee[1].FirstName, employee3.FirstName);
            Assert.AreEqual(listExtEmployee[1].ContactExpiration.ToShortDateString(), employee3.ContactExpiration.ToShortDateString());
            Assert.AreEqual(listExtEmployee[1].ConsultantName, employee3.ConsultantName);
        }

        [TestMethod]
        public void GetAllExternal_UsandoIs_Employee()
        {

            InitializeTestData();

            //
            //recupero solo empleados externos
            //
            IEmployeeRepository repoExternalEmployee = new EmployeeRepository();

            List<Employee> listExtEmployee = repoExternalEmployee.GetAllExternalType();

            //
            // Assert
            //
            Assert.AreEqual(listExtEmployee.Count, 2);
            
            Assert.IsInstanceOfType(listExtEmployee[0], typeof(EmployeeExternal));
            Assert.IsInstanceOfType(listExtEmployee[1], typeof(EmployeeExternal));

            Assert.AreEqual(listExtEmployee[0].FirstName, employee2.FirstName);
            Assert.AreEqual(((EmployeeExternal)listExtEmployee[0]).ContactExpiration.ToShortDateString(), employee2.ContactExpiration.ToShortDateString());
            Assert.AreEqual(((EmployeeExternal)listExtEmployee[0]).ConsultantName, employee2.ConsultantName);

            Assert.AreEqual(listExtEmployee[1].FirstName, employee3.FirstName);
            Assert.AreEqual(((EmployeeExternal)listExtEmployee[1]).ContactExpiration.ToShortDateString(), employee3.ContactExpiration.ToShortDateString());
            Assert.AreEqual(((EmployeeExternal)listExtEmployee[1]).ConsultantName, employee3.ConsultantName);
        }

    }
}
