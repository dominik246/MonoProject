using System.ComponentModel.DataAnnotations;

namespace Project.MVC.Models
{
    public class VehicleMakeDTO : IVehicle
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Make Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Abbreviation")]
        public string Abrv { get; set; }
    }
}
