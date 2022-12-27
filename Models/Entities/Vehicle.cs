using System.ComponentModel.DataAnnotations;

namespace GarageMVC.Models.Entities
{
    public class Vehicle
    {
        public int Id
        {
            get; set;
        }
        public VehicleType VehicleType
        {
            get; set;
        }

        public string RegNo
        {
            get; set;
        } = string.Empty;
        public string Color
        {
            get; set;
        } = string.Empty;

        public string Brand
        {
            get; set;
        } = string.Empty;
        public string Model
        {
            get; set;
        } = string.Empty;
        public int NoOfWheels
        {
            get; set;
        }
        public DateTime TimeOfArrival
        {
            get; set;
        } = DateTime.Now;
    }
}
