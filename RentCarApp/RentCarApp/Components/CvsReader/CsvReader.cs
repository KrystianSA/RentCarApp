using RentCarApp.Components.CvsReader;
using RentCarApp.Components.CvsReader.Extensions;
using RentCarApp.Components.Models;
using RentCarApp.Repositories;

namespace RentCarApp.Components.CvsReader
{
    public class CsvReader : ICsvReader
    {

        public List<Car> ProcessCars(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Car>();
            }

            var cars = File.ReadAllLines(filePath)
                .Skip(1)
                .Where(x => x.Length > 1)
                .ToCar();

            return cars.ToList();
        }

        public List<Manufacturer> ProcessManufacturers(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Manufacturer>();
            }

            var manufacturer = File.ReadAllLines(filePath)
                .Where(x => x.Length > 1)
                .ToManufacturer();

            return manufacturer.ToList();
        }
    }
}
