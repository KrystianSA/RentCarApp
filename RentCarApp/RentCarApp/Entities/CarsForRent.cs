using RentCarApp.Entities;

namespace RentCarApp.Entities
{
    public class CarsForRent : EntityBase
    {
        public override string ToString() => $"Id {Id}. {carBrand + carModel}";
    }
}