namespace GarageMVC.Models.Entities
{
    public class Vehicle
    {
        public int Id
        {
            get; set;
        }
        public string? VehicleType
        {
            get; set;
        }

        public string? RegNo
        {
            get; set;
        }
        public string? Color
        {
            get; set;
        }

        public string? Brand 
        {
            get; set;
        }
        public string? Model
        {
            get; set;
        }
        public int NoOfWheels
        {
            get; set;
        }
        public DateTime TimeOfArrival
        {
            get;
        }
    }
}
