using RentCarApp.Repositories;
using RentCarApp.Entities;
using RentCarApp.Components.CvsReader;
using System.Xml.Linq;
using RentCarApp.Data;
using AutoMapper;
using RentCarApp.Components.XmlCreator;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RentCarApp.Components.Models;

namespace RentCarApp.Services
{
    public class App : IApp
    {
        private readonly IUserCommunication _userCommunication;
        private readonly ICarDataProvider _carDataProvider;
        private readonly IEventHandlerService _eventHandlerService;
        private readonly ICsvReader _csvReader;
        private readonly RentCarAppDbContext _rentCarAppDbContext;
        private readonly IMapper _mapper;

        public App(IUserCommunication userCommunication,
            ICarDataProvider dataProvider,
            IEventHandlerService eventHandlerService,
            ICarDataSelector dataSelector,
            ICsvReader csvReader,
            RentCarAppDbContext rentCarAppDbContext,
            IMapper mapper)
        {
            _userCommunication = userCommunication;
            _carDataProvider = dataProvider;
            _eventHandlerService = eventHandlerService;
            _csvReader = csvReader;
            _rentCarAppDbContext = rentCarAppDbContext;
            _mapper = mapper;
            _rentCarAppDbContext.Database.EnsureCreated();
        }

        public void Run()
        {
            Console.WriteLine("Witaj w aplikacji do zarządzania wypożyczalnią samochodów!");
            Console.WriteLine("----------------------------------------------------------");

            InsertDataToSql();
            _carDataProvider.AddCars();
            _eventHandlerService.EventHandlerForList();
            _userCommunication.ChooseWhatToDo();
        }

        private void InsertDataToSql()
        {
            var cars = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");

            foreach (var car in cars)
            {
                _rentCarAppDbContext.CarsCatalog.Add(new CarsCatalog()
                {
                    Brand = car.Manufacturer,
                    Model = car.Model,
                });

                _rentCarAppDbContext.SaveChanges();
            }
        }
    }
}