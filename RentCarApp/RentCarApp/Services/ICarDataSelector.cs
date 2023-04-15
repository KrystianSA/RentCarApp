using RentCarApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentCarApp.Repositories;
using RentCarApp.Services;

namespace RentCarApp.Services
{
    public interface ICarDataSelector
    {
        List<Car> WhereColorIs(string color);
        List<Car> WhereBrandIs(string brand);
        List<Car> OrderByPriceAscending();
        List<string> GetUniqueCarColors();
        decimal GetMinimumPriceOfAllCars();
        decimal GetMaximumPriceOfAllCars();
        decimal GetMinimumPowerOfAllCars();
        decimal GetMaximumPowerOfAllCars();
    }
}
