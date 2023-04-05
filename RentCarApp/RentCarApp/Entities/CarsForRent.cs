using RentCarApp.Entities;

namespace RentCarApp.Entities
{
    public class CarsForRent : Cars
    {
        public override string ToString() => $"Id {Id}. {carBrand + carModel}";
    }
}