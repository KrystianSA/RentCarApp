using RentCarApp.Repositories;
using RentCarApp.Entities;

namespace RentCarApp.Services
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<Car> _carRepository;
        private readonly IDataSelector _dataSelector;

        public UserCommunication(IRepository<Car> carRepository, IDataSelector dataSelector)
        {
            _carRepository = carRepository;
            _dataSelector = dataSelector;
        }

        public void ChooseWhatToDo()
        {

            while (true)
            {
                Console.WriteLine("\n1. Lista samochodów");
                Console.WriteLine("2. Przeszukaj bazę");
                Console.WriteLine("3. Dodaj samochód do listy");
                Console.WriteLine("4. Usuń samochód z listy");
                Console.WriteLine("5. Zapisz dane do pliku");
                Console.WriteLine("6. Wyjście\n");
                Console.Write("Wybór : ");
                var choice = Console.ReadLine();
                Console.WriteLine(" ");
                switch (choice)
                {
                    case "1":
                        WriteAllToConsole(_carRepository);
                        break;
                    case "2":
                        SearchSpecificElementInBase(_carRepository,_dataSelector);
                        break;
                    case "3":
                        AddNewCar(_carRepository);
                        break;
                    case "4":
                        RemoveCarAndSortList(_carRepository);
                        break;
                    case "5":
                        WriteToFileTxt(_carRepository);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Nie ma takiej opcji");
                        break;
                }
            }

            static void WriteAllToConsole<T>(IRepository<T> carRepository) where T : class, IEntity
            {
                var cars = carRepository.GetAll();
                foreach (var car in cars)
                {
                    Console.WriteLine($"\n{car}");
                }
            }

            static void AddNewCar(IRepository<Car> newCar)
            {
                Console.Write("Podaj markę samochodu: ");
                var brand = Console.ReadLine();
                Console.Write("Podaj model samochodu: ");
                var model = Console.ReadLine();
                Console.Write("Podaj kolor samochodu: ");
                var color = Console.ReadLine();
                Console.Write("Podaj cenę wynajęcia za dzień [zł] : ");
                var priceForDay = Console.ReadLine();
                Console.Write("Podaj moc samochodu [KM] : ");
                var power = Console.ReadLine();
                var car = new Car { Brand = brand, Model = model, Color = color,
                    PriceForDay = decimal.Parse(priceForDay),
                    Power = decimal.Parse(power) };

                Console.WriteLine("Czy samochód jest wypożyczony / zwrócony / wolny? (t/n/w)");
                var isRented = Console.ReadLine();
                switch (isRented)
                {
                    case "t":
                        car.IsRented = true;
                        break;
                    case "n":
                        car.IsReturned = true;
                        break;
                    case "w":
                        break;
                    default:
                        break;
                }
                newCar.Add(car);
                newCar.Save();
            }

            static void RemoveCarAndSortList(IRepository<Car> removeCar)
            {
                Console.Write("Podaj numer id samochodu do usunięcia: ");
                var id = int.Parse(Console.ReadLine());
                var car = removeCar.GetById(id);
                removeCar.Remove(car);
                removeCar.SortElements();
                removeCar.Save();
            }

            void WriteToFileTxt(IRepository<Car> carRepository)
            {
                DeleteFileTxt();
                var cars = carRepository.GetAll();
                using (var writer = new StreamWriter("Lista Samochodów.txt"))
                {
                    foreach (var car in cars)
                    {
                        writer.WriteLine(car);
                    }
                }
            }

            void DeleteFileTxt()
            {
                if (File.Exists("Lista Samochodów.txt"))
                {
                    File.Delete("Lista Samochodów.txt");
                }
            }

            void SearchSpecificElementInBase(IRepository<Car> carRepository, IDataSelector dataSelector)
            {
                Console.WriteLine(" 1. Maksymalna Moc");
                Console.WriteLine(" 2. Minimalna  Moc");
                Console.WriteLine(" 3. Maksymalna Cena");
                Console.WriteLine(" 4. Minimalna  Cena");
                Console.WriteLine(" 5. Wszystkie dostępne kolory");
                Console.WriteLine(" 6. Posegreguj po cenie rosnąco");
                Console.WriteLine(" 7. Wyszukaj po Marce");
                Console.WriteLine(" 8. Wyszukaj po kolorze\n");

                Console.Write("Wybór : ");
                var choice = Console.ReadLine();

                switch (choice) 
                {
                    case "1":
                        Console.WriteLine($"\n{dataSelector.GetMaximumPowerOfAllCars()} KM");
                        break;
                    case "2":
                        Console.WriteLine($"\n{dataSelector.GetMinimumPowerOfAllCars()} KM");
                        break;
                    case "3":
                        Console.WriteLine($"\n{dataSelector.GetMaximumPriceOfAllCars()} zł");
                        break;
                    case "4":
                        Console.WriteLine($"\n{dataSelector.GetMinimumPriceOfAllCars()} zł");
                        break;
                    case "5":
                        Console.WriteLine("");
                        foreach (var colors in _dataSelector.GetUniqueCarColors())
                        {
                            Console.Write($" {colors} /");
                        }
                        Console.WriteLine("");
                        break;
                    case "6":
                        Console.WriteLine("");
                        foreach (var price in _dataSelector.OrderByPriceAscending())
                        {
                            Console.WriteLine($"{price} zł");
                        }
                        Console.WriteLine("");
                        break;
                    case "7":
                        Console.WriteLine("\nPodaj markę samochodu. " +
                            "              Dostępne : Seat / Audi / Mercedes / BMW");
                        var brand = Console.ReadLine();
                        foreach (var car in _dataSelector.WhereBrandIs(brand))
                        {
                            Console.WriteLine(car);
                        }
                        break;
                    case "8":
                        Console.WriteLine("\nPodaj kolor. " +
                            "              Dostępne : Czerwony / Czarny / Biały ");
                        var color = Console.ReadLine();
                        foreach (var car in _dataSelector.WhereColorIs(color))
                        {
                            Console.WriteLine(car);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
