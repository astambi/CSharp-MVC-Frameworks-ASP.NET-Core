namespace CarDealer.Web.Models.Cars
{
    using Services.Models.Cars;

    public class CarByIdWithPartsModel
    {
        public int Id { get; set; }

        public CarWithPartsModel Car { get; set; }
    }
}
