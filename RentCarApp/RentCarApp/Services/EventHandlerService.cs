using RentCarApp.Repositories;
using RentCarApp.Entities;

namespace RentCarApp.Services
{
    public class EventHandlerService : IEventHandlerService
    {
        private readonly IRepository<Car> _carRepository;

        public EventHandlerService(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public void EventHandlerForList()
        {
            _carRepository.ItemAdded += EventHandlerForCarOnItemAddedToFileTxt;
            _carRepository.ItemRemoved += EventHandlerForCarOnItemRemovedToFileTxt;
            _carRepository.ItemsSaveToFile += EventHandlerForCarOnItemsSaveToFileTxt;
        }

        private void EventHandlerForCarOnItemsSaveToFileTxt(object? sender, Car e)
        {
            Console.WriteLine("===> Zapisano do pliku <===");
        }

        private void EventHandlerForCarOnItemAddedToFileTxt(object? sender, Car e)
        {
            SaveInfoAboutEventToFile("Car Added", e);
        }

        private void EventHandlerForCarOnItemRemovedToFileTxt(object? sender, Car e)
        {
            SaveInfoAboutEventToFile("Car Removed", e);
        }

        private void SaveInfoAboutEventToFile(string info, Car e)
        {
            using (var writer = new StreamWriter("Historia.txt", true))
            {
                writer.WriteLine($"[{DateTime.Now}] - {info} - {e.Brand} {e.Model}");
            }
        }
    }
}