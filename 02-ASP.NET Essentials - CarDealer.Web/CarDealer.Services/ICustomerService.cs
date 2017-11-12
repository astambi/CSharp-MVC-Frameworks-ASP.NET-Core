namespace CarDealer.Services
{
    using Models;
    using Models.Customers;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> AllOrdered(OrderDirection order);

        CustomerTotalSalesModel TotalSalesById(int id);
    }
}
