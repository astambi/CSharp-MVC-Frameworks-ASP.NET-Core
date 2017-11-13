namespace CarDealer.Services.Models.Cars
{
    using Models.Parts;
    using System.Collections.Generic;

    public class CarWithPartsModel : CarModel
    {
        public IEnumerable<PartModel> Parts { get; set; }
    }
}
