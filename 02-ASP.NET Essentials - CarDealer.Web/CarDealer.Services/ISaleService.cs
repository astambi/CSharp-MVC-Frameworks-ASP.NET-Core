namespace CarDealer.Services
{
    using Models.Sales;
    using System.Collections.Generic;

    public interface ISaleService
    {
        IEnumerable<SaleModel> All();

        IEnumerable<SaleModel> Discounted(int? discount);

        SaleModel ById(int id);
    }
}
