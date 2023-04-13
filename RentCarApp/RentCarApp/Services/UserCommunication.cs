using RentCarApp.Repositories;
using RentCarApp.Entities;

namespace RentCarApp.Services
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<Car> _carRepository;

        public UserCommunication(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public void ChooseWhatToDo()
        {

            while (true)
            {
                Console.WriteLine("\n1. Lista samochodów");
                Console.WriteLine("2. Dodaj samochód do listy");
                Console.WriteLine("3. Usuń samochód z listy");
                Console.WriteLine("4. Zapisz dane do pliku");
                Console.WriteLine("5. Wyjście\n");
                Console.Write("Wybór : ");
                var choice = Console.ReadLine();
                Console.WriteLine(" ");
                switch (choice)
                {
                    case "1":
                        WriteAllToConsole(_carRepository);
                        break;
                    case "2":
                        AddNewCar(_carRepository);
                        break;
                    case "3":
                        RemoveCarAndSortList(_carRepository);
                        break;
                    case "4":
                        WriteToFileTxt(_carRepository);
                        break;
                    case "5":
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
        }
    }
}
