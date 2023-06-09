﻿using RentCarApp.Repositories;
using RentCarApp.Services;
using RentCarApp.Entities;
using RentCarApp.Repositories.RepositoryExtensions;
using System.Security.Cryptography.X509Certificates;

namespace RentCarApp.Services
{
    public class CarDataProvider : ICarDataProvider
    {
        private readonly IRepository<Car> _carRepository;

        public CarDataProvider(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public void AddCars()
        {
            var car = new Car[]
                {
                    new Car{Brand = "Seat", Model = "Leon", Color = "Czerwony", PriceForDay = 85, Power = 120, IsReadyToRent = true},
                    new Car{Brand = "Audi", Model = "A6", Color = "Czarny", PriceForDay = 100, Power = 150, IsReadyToRent = true},
                    new Car{Brand = "Audi", Model = "A8", Color = "Biały", PriceForDay = 150, Power = 200, IsReadyToRent = true},
                    new Car{Brand = "BMW", Model = "E36", Color = "Czerwony", PriceForDay = 70, Power = 74, IsReadyToRent = true},
                    new Car{Brand = "BMW", Model = "E46", Color = "Czarny", PriceForDay = 100, Power = 150, IsReadyToRent = true},
                    new Car{Brand = "BMW", Model = "E60", Color = "Biały", PriceForDay = 150, Power = 200, IsReadyToRent = true},
                    new Car{Brand = "Mercedes", Model = "C180", Color = "Czerwony", PriceForDay = 70, Power = 105, IsReadyToRent = true},
                    new Car{Brand = "Mercedes", Model = "C200", Color = "Czarny", PriceForDay = 100, Power = 150, IsReadyToRent = true},
                    new Car{Brand = "Mercedes", Model = "C180", Color = "Czerwony", PriceForDay = 70, Power = 90 , IsReadyToRent = true},
                    new Car{Brand = "Mercedes", Model = "C180", Color = "Czerwony", PriceForDay = 70, Power = 80 , IsReadyToRent = true}
            };
            _carRepository.AddBatch(car);
        }
    }
}

