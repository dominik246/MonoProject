using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Service.Models
{
    public class VehicleModel : IModel
    {
        public int Id { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }

        [ForeignKey("VehicleMake")]
        public int MakeId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string Abrv { get; set; }
    }
}
