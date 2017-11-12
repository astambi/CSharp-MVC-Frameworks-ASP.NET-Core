namespace CarDealer.Web.Models.Customers
{
    using Services.Models;
    using Services.Models.Customers;
    using System.Collections.Generic;

    public class AllCustomersModel
    {
        public IEnumerable<CustomerModel> Customers { get; set; }

        public OrderDirection OrderDirection { get; set; }
    }
}
