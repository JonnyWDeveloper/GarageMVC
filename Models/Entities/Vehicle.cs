using System.ComponentModel.DataAnnotations;

namespace GarageMVC.Models.Entities
{
    public class Vehicle
    {
        public int Id
        {
            get; set;
        }

        [Required]
        public VehicleType VehicleType
        {
            get; set;
        }

        [Required]
        [RegularExpression(@"^[A-Za-z]{3}(\d{3})$", ErrorMessage = "ABC123 format expected")]
        public string RegNo
        {
            get; set;
        } = string.Empty;


        [Required]
        [StringLength(16)]
        public string Color
        {
            get; set;
        } = string.Empty;

        [Required]
        [StringLength(16)]
        public string Brand
        {
            get; set;
        } = string.Empty;


        [Required]
        [StringLength(16)]
        public string Model
        {
            get; set;
        } = string.Empty;

        [Required]
        [Range(0, 6)]
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
