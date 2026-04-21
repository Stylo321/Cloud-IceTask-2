using System.ComponentModel.DataAnnotations;

namespace Kias_Kar_Kompany.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public required string VehicleName { get; set; }
        public required string VehicleModel { get; set; }
        public required int VehiclePrice { get; set; }
        public required string VehicleType { get; set; } = "Sedan";
        public string? VehicleImageURL { get; set; }

        public int ManufacturerId { get; set; }
       
        public Manufacturer? Manufacturer { get; set; }
    }
}
