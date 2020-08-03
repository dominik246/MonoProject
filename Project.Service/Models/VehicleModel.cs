using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Service.Models
{
    public class VehicleModel : IModel
    {
        [Display(Name = "ID")]
        [Key]
        public int Id { get; set; }

        public virtual VehicleMake SelectedVehicleMake { get; set; }

        [Display(Name = "Make Name")]
        [ForeignKey(nameof(SelectedVehicleMake))]
        public int MakeId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Model Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Abbreviation")]
        public string Abrv { get; set; }
    }
}
