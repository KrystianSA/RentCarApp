using RentCarApp.Repositories;
using RentCarApp.Entities;
using RentCarApp.Components.CvsReader;
using System.Xml.Linq;

namespace RentCarApp.Services
{
    public class App : IApp
    {
        private readonly IUserCommunication _userCommunication;
        private readonly ICarDataProvider _dataProvider;
        private readonly IEventHandlerService _eventHandlerService;
        private readonly ICsvReader _csvReader;

        public App(IUserCommunication userCommunication,
            ICarDataProvider dataProvider,
            IEventHandlerService eventHandlerService,
            ICarDataSelector dataSelector,
            ICsvReader csvReader)
        {
            _userCommunication = userCommunication;
            _dataProvider = dataProvider;
            _eventHandlerService = eventHandlerService;
            _csvReader = csvReader;
        }

        public void Run()
        {
            Console.WriteLine("Witaj w aplikacji do zarządzania wypożyczalnią samochodów!");
            Console.WriteLine("----------------------------------------------------------");

            _eventHandlerService.EventHandlerForList();
            _dataProvider.AddCars();
            _userCommunication.ChooseWhatToDo();      
        }
    }
}