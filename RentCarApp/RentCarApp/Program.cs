using RentCarApp.Entities;
using RentCarApp.Repositories;
using RentCarApp.Data;
var carRepository = new SqlRepository<CarsForRent>(new RentCarAppDbContext());
carRepository.Save();

static void CarsForRent(IRepository<CarsForRent> carRepository)
{
    carRepository.Add(new CarsForRent { carBrand = "Seat ", carModel = "Leon" });
    carRepository.Save();
}
static void ReturnedCar(IWriteRepository<ReturnedCars> carRepository3)
{
    carRepository3.Add(new ReturnedCars { carBrand = "Fiat ", carModel = "Seicento" });
    carRepository3.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> carRepository)
{
    var cars = carRepository.GetAll();
    foreach (var car in cars)
    {
        Console.WriteLine(car);
    }
}

CarsForRent(carRepository);
ReturnedCar(carRepository);
WriteAllToConsole(carRepository);