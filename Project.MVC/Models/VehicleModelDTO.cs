using System.ComponentModel.DataAnnotations;

namespace Project.MVC.Models
{
    public class VehicleModelDTO : IVehicle
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Vehicle Make")]
        public VehicleMakeDTO SelectedVehicleMake { get; set; }

        [Display(Name = "Make Name")]
        public int MakeId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Model Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Abbreviation")]
        public string Abrv { get; set; }
    }
}
