using RentCarApp.Repositories;
using RentCarApp.Entities;
using RentCarApp.Components.CvsReader;
using RentCarApp.Components.XmlCreator;
using System.Xml.Linq;
using RentCarApp.Data;
using System.Xml.XPath;

namespace RentCarApp.Services
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<Car> _carRepository;
        private readonly ICarDataSelector _dataSelector;
        private readonly RentCarAppDbContext _rentCarAppDbContext;

        public UserCommunication(
            IRepository<Car> carRepository,
            ICarDataSelector dataSelector,
            RentCarAppDbContext rentCarAppDbContext)
        {
            _carRepository = carRepository;
            _dataSelector = dataSelector;
            _rentCarAppDbContext = rentCarAppDbContext;
        }
        public void ChooseWhatToDo()
        {

            while (true)
            {
                Console.WriteLine("\n1. Lista dostępnych samochodów");
                Console.WriteLine("2. Przeszukaj listę");
                Console.WriteLine("3. Dodaj samochód do listy");
                Console.WriteLine("4. Usuń samochód z listy");
                Console.WriteLine("5. Edytuj dane samochodu");
                Console.WriteLine("6. Katalog pojazdów na zamówienie");
                Console.WriteLine("7. Wyjście\n");
                Console.Write("Wybór : ");
                var choice = Console.ReadLine();
                Console.WriteLine(" ");
                switch (choice)
                {
                    case "1":
                        WriteAllToConsole(_carRepository);
                        break;
                    case "2":
                        SearchSpecificElementInBase(_carRepository, _dataSelector);
                        break;
                    case "3":
                        AddNewCar(_carRepository);
                        break;
                    case "4":
                        RemoveCar(_carRepository);
                        break;
                    case "5":
                        EditCar(_carRepository);
                        break;
                    case "6":
                        Console.Write("Wybierz catalog z bazy SQL lub pliku XML : ");
                        var catalog = Console.ReadLine();
                        if (catalog.ToLower() == "sql")
                        {
                            CarCatalogFromSql(_rentCarAppDbContext);
                        }
                        else if (catalog.ToLower() == "xml")
                        {
                            CarCatalogFromXml();
                        }
                        else
                        {
                            Console.WriteLine("Nie ma takiej opcji");
                        }
                        CarCatalogFromXml();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Nie ma takiej opcji");
                        break;
                }
            }

            static void WriteAllToConsole<T>(IRepository<T> carRepository) where T : class, IEntity
            {
                var cars = carRepository.Read();

                foreach (var car in cars)
                {
                    Console.WriteLine(car);
                    Console.WriteLine();
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
                var car = new Car
                {
                    Brand = brand,
                    Model = model,
                    Color = color,
                    PriceForDay = decimal.Parse(priceForDay),
                    Power = decimal.Parse(power)
                };

                Console.Write("Czy samochód jest wypożyczony / zwrócony / do wypożyczenia? (t/n/w) : ");
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


            static void RemoveCar(IRepository<Car> removeCar)
            {
                removeCar.Read();
                Console.Write("Podaj numer id samochodu do usunięcia: ");
                var id = int.Parse(Console.ReadLine());
                var car = removeCar.GetById(id);
                removeCar.Remove(car);
                removeCar.Save();
            }

            void SearchSpecificElementInBase(IRepository<Car> carRepository, ICarDataSelector dataSelector)
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

            static void EditCar(IRepository<Car> carRepository)
            {
                var cars = carRepository.GetAll();

                Console.WriteLine("Elementy które mogą zostać edytowane to cena za dzień wynajmu");
                Console.Write("Podaj numer id samochodu do edycji: ");
                var id = int.Parse(Console.ReadLine());
                var car = carRepository.GetById(id);

                Console.Write("Podaj nową cenę wynajęcia za dzień [zł] : ");
                var price = int.Parse(Console.ReadLine());

                car.PriceForDay = price;
                carRepository.Save();

            }

            static void CarCatalogFromSql(RentCarAppDbContext rentCarAppDbContext)
            {
                var cars = rentCarAppDbContext.CarsCatalog
                    .Select(x => x.Brand)
                    .Distinct()
                    .ToList();

                foreach (var car in cars)
                {
                    Console.WriteLine(car);
                }

                Console.Write("\nWybierz markę samochodu : ");
                Console.WriteLine();
                var brand = Console.ReadLine();

                if (brand != null)
                {
                    var carGroups = rentCarAppDbContext.CarsCatalog
                        .Where(x => x.Brand == brand.ToLower())
                        .GroupBy(x => x.Model)
                        .ToList();

                    foreach (var carGroup in carGroups)
                    {
                        Console.WriteLine($"\t{carGroup.Key}");
                    }
                }
                else
                {
                    Console.WriteLine("Nieprawidłowa wartość");
                }
            }

            static void CarCatalogFromXml()
            {
                var document = XDocument.Load("Katalog Samochodów.xml");

                var cars = document
                    .Elements("Manufacturers")?
                    .Elements("Manufacturer")
                    .Select(x => x.Attribute("Name")?.Value);

                foreach (var car in cars)
                {
                    Console.WriteLine(car);
                }

                Console.Write("\nWybierz dostępną markę samochodu : \n");
                var brand = Console.ReadLine();
                if (brand != null)
                {
                    var models = document
                    .Elements("Manufacturers")?
                    .Elements("Manufacturer")
                    .Where(x => x.Attribute("Name")?.Value == brand)
                    .Elements("Cars")?
                    .Elements("Car")?
                    .Elements("Car")
                    .Select(x => x.Attribute("Model")?.Value)
                    .Distinct();

                    foreach (var model in models)
                    {
                        Console.WriteLine($"\nModel : {model}");
                    }
                }
            }
        }
    }
}