using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Service.Models
{
    public class VehicleMake : IModel
    {
        [Display(Name = "ID")]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Make Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "Abbreviation")]
        public string Abrv { get; set; }

        private ICollection<VehicleModel> _vehicleModelCollection;
        public virtual ICollection<VehicleModel> VehicleModelCollection
        {
            get { return _vehicleModelCollection ??= new Collection<VehicleModel>(); }
            protected set { _vehicleModelCollection = value; }
        }
    }
}
