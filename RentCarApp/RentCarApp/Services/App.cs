using RentCarApp.Repositories;
using RentCarApp.Entities;

namespace RentCarApp.Services
{
    public class App : IApp
    {
        private readonly IUserCommunication _userCommunication;
        private readonly IDataProvider _dataProvider;
        private readonly IEventHandlerService _eventHandlerService;
        private readonly IDataSelector _dataSelector;

        public App(IUserCommunication userCommunication,
            IDataProvider dataProvider,
            IEventHandlerService eventHandlerService,
            IDataSelector dataSelector)
        {
            _userCommunication = userCommunication;
            _dataProvider = dataProvider;
            _eventHandlerService = eventHandlerService;
            _dataSelector = dataSelector;
        }

        public void Run()
        {
            Console.WriteLine("Witaj w aplikacji do zarządzania wypożyczalnią samochodów!");
            Console.WriteLine("----------------------------------------------------------");

            _eventHandlerService.EventHandlerForList();
            _dataProvider.AddCars();
            _userCommunication.ChooseWhatToDo();

            // dodać do Usera możliwość filtrowania 
        }
    }
}