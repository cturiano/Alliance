﻿using System;
using System.IO;
using System.Reflection;
using Alliance;
using Alliance.Models;
using NUnit.Framework;

namespace AllianceTest
{
    [TestFixture]
    public class FinalTest
    {
        #region Setup/Teardown

        [TearDown]
        public void TearDown()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));

            if (!string.IsNullOrEmpty(path))
            {
                File.Delete(Path.Combine(path, "Customer.json"));
                File.Delete(Path.Combine(path, "Company.json"));
            }
        }

        #endregion

        [TestCase]
        public void ProgrammerTest()
        {
            var address = new Address("56 Main St", "Mesa", "AZ", "38574");
            var customer = new Customer("John", "Doe", address);
            var company = new Company("Google", address);

            Assert.IsNullOrEmpty(customer.Id);
            customer.Save();
            Assert.IsNotNullOrEmpty(customer.Id);

            Assert.IsNullOrEmpty(company.Id);
            company.Save();
            Assert.IsNotNullOrEmpty(company.Id);

            var savedCustomer = Customer.Find(customer.Id);
            Assert.IsNotNull(savedCustomer);
            Assert.AreSame(customer.Address, address);
            Assert.AreEqual(savedCustomer.Address, address);
            Assert.AreEqual(customer.Id, savedCustomer.Id);
            Assert.AreEqual(customer.FirstName, savedCustomer.FirstName);
            Assert.AreEqual(customer.LastName, savedCustomer.LastName);
            Assert.AreEqual(customer, savedCustomer);
            Assert.AreNotSame(customer, savedCustomer);

            var savedCompany = Company.Find(company.Id);
            Assert.IsNotNull(savedCompany);
            Assert.AreSame(company.Address, address);
            Assert.AreEqual(savedCompany.Address, address);
            Assert.AreEqual(company.Id, savedCompany.Id);
            Assert.AreEqual(company.Name, savedCompany.Name);
            Assert.AreEqual(company, savedCompany);
            Assert.AreNotSame(company, savedCompany);

            customer.Delete();
            Assert.IsNullOrEmpty(customer.Id);
            Assert.IsNull(Customer.Find(customer.Id));

            company.Delete();
            Assert.IsNullOrEmpty(company.Id);
            Assert.IsNull(Company.Find(company.Id));
        }
    }
}