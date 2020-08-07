namespace Project.Service.Models
{
    public interface IVehicle
    {
        int Id { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
        VehicleMake SelectedVehicleMake { get; set; }
    }
}
