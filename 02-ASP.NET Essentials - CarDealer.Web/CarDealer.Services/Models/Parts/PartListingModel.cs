namespace CarDealer.Services.Models.Parts
{
    public class PartListingModel : PartModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public string Supplier { get; set; }
    }
}
