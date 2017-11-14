namespace CarDealer.Services
{
    using Models.Cars;
    using System.Collections.Generic;

    public interface ICarService
    {
        IEnumerable<CarModel> AllByMake(string make);

        IEnumerable<CarWithPartsModel> AllWithParts();

        IEnumerable<CarBasicModel> AllBasic();

        void Create(string make, string model, long travelledDistance, 
                    IEnumerable<int> selectedPartIds);
    }
}
