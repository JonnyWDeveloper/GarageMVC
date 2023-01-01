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
              
        public DateTime TimeOfArrival
        {
            get; set;
        } = DateTime.Now;
    }
}
