namespace CarDealer.Services
{
    using Models.Parts;
    using System.Collections.Generic;

    public interface IPartService
    {
        IEnumerable<PartListingModel> All(int page = 1, int pageSize = 10);

        IEnumerable<PartBasicModel> AllBasic();

        void Create(string name, decimal price, int quantity, int supplierId);

        void Delete(int id);

        void Update(int id, decimal price, int quantity);

        PartEditModel GetById(int id);

        int Total();
    }
}
