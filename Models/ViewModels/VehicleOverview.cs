using System.ComponentModel.DataAnnotations;
using GarageMVC.Models.Entities;

namespace GarageMVC.Models.ViewModels
{
    public class VehicleOverview
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
