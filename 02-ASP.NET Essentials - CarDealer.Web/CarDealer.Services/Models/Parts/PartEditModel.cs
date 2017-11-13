namespace CarDealer.Services.Models.Parts
{
    using System.ComponentModel.DataAnnotations;

    public class PartEditModel
    {
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
