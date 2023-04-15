using RentCarApp.Entities;
using RentCarApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApp.Services
{
    public class CarDataSelector : ICarDataSelector
    {
        private readonly IRepository<Car> _carRepository;

        public CarDataSelector(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }
        public decimal GetMaximumPowerOfAllCars()
        {
            var cars = _carRepository.GetAll();
            return cars.Select(x => x.Power).Max();
        }

        public decimal GetMinimumPowerOfAllCars()
        {
            var cars = _carRepository.GetAll();
            return cars.Select(x => x.Power).Min();
        }

        public decimal GetMaximumPriceOfAllCars()
        {
            var cars = _carRepository.GetAll();
            return cars.Select(x => x.PriceForDay).Max();
        }

        public decimal GetMinimumPriceOfAllCars()
        {
            var cars = _carRepository.GetAll();
            return cars.Select(x => x.PriceForDay).Min();
        }

        public List<Car> WhereColorIs(string color)
        {
            var cars = _carRepository.GetAll();
            return cars.Where(x => x.Color == color).ToList();
        }

        public List<Car> WhereBrandIs(string brand)
        {
            var cars = _carRepository.GetAll();
            return cars.Where(x => x.Brand == brand).ToList();
        }

        public List<Car> OrderByPriceAscending()
        {
            var cars = _carRepository.GetAll();
            return cars.OrderBy(x => x.PriceForDay).ToList();
        }

        public List<string> GetUniqueCarColors()
        {
            var cars = _carRepository.GetAll();
            var color = cars.Select(x => x.Color).Distinct().ToList();
            return color;
        }
    }
}
