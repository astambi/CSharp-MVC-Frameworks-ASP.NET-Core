namespace CarDealer.Services
{
    using Models;
    using Models.Customers;
    using System;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> AllOrdered(OrderDirection order);

        IEnumerable<CustomerBasicModel> AllBasic();

        CustomerTotalSalesModel TotalSalesById(int id);

        void Create(string name, DateTime birthDate, bool isYoungDriver);

        CustomerModel GetById(int id);

        void Update(int id, string name, DateTime birthDate, bool isYoungDriver);

        bool Exists(int id);
    }
}
