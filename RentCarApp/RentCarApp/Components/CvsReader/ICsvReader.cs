using RentCarApp.Components.Models;

namespace RentCarApp.Components.CvsReader
{
    public interface ICsvReader
    {
        List<Car> ProcessCars(string filePath);

        List<Manufacturer> ProcessManufacturers(string filePath);
    }
}
