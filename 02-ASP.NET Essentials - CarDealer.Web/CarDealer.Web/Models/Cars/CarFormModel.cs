namespace CarDealer.Web.Models.Cars
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CarFormModel
    {
        [Required]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        [MaxLength(50)]
        public string Model { get; set; }

        [Range(0, long.MaxValue, ErrorMessage = "Distance must be a positive number")]
        [Display(Name = "Travelled Distance")]
        public long TravelledDistance { get; set; }

        public IEnumerable<int> SelectedParts { get; set; }

        [Display(Name = "Parts")]
        public IEnumerable<SelectListItem> PartsDropdown { get; set; }

    }
}
