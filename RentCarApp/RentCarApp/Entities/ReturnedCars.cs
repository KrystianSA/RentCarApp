using RentCarApp.Entities;


namespace RentCarApp.Entities
{
    public class ReturnedCars : CarsForRent
    {
        public override string ToString() => base.ToString() + " => zwrócony";
    }
}