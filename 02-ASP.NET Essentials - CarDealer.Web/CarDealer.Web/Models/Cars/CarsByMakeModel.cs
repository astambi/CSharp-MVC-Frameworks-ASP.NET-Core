namespace CarDealer.Web.Models.Cars
{
    using Services.Models.Cars;
    using System.Collections.Generic;

    public class CarsByMakeModel
    {
        public IEnumerable<CarModel> Cars { get; set; }

        public string Make { get; set; }
    }
}
