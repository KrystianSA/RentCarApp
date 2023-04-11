using RentCarApp.Entities;
using RentCarApp.Repositories;
using RentCarApp.Repositories.RepositoryExtensions;

void EventHandlerForCarOnItemAddedToFileTxt(object? sender, Car e)
{
    using (var writer = new StreamWriter("Historia.txt",true))
    {
        writer.WriteLine($"[{DateTime.Now}] - Dodano Samochód - {e.Brand} {e.Model}");
    }
}
void EventHandlerForCarOnItemRemovedToFileTxt(object? sender, Car e)
{
    using (var writer = new StreamWriter("Historia.txt", true))
    {
        writer.WriteLine($"[{DateTime.Now}] - Usunięto Samochód - {e.Brand} {e.Model}");
    }
}

Console.WriteLine("Witaj w aplikacji do zarządzania wypożyczalnią samochodów!");
Console.WriteLine("----------------------------------------------------------");

var cars = new FileRepository<Car>();
AddCars(cars);
EventHandlerForList(cars);

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
            WriteAllToConsole(cars);
            break;
        case "2":
            AddNewCar(cars);
            break;
        case "3":
            RemoveCarAndSortList(cars);
            break;
        case "4":
            WriteToFileTxt(cars);
            break;
        case "5":
            return;
        default:
            Console.WriteLine("Nie ma takiej opcji");
            break;
    }
}

void EventHandlerForList(FileRepository<Car> cars)
{
    cars.ItemAdded += EventHandlerForCarOnItemAddedToFileTxt;
    cars.ItemRemoved += EventHandlerForCarOnItemRemovedToFileTxt;
}

static void AddCars(IRepository<Car> carRepository)
{
    var car = new Car[]
        {
        new Car("Seat", " Leon"),
        new Car("Audi", " A4"),
        new Car("BMW", " X5"),
        new Car("Mercedes", " C200"),
        new Car("Volkswagen", " Golf"),
        new Car("Fiat", " Panda"),
        new Car("Fiat",  "Punto"),
        new Car("Peugeot", " 407"),
        new Car("Opel", " Astra"),
        new Car("Ford", " Focus"),
    };
    carRepository.AddBatch(car);
}

static void WriteAllToConsole<T>(IRepository<T> carRepository) where T : class, IEntity
{
    var cars = carRepository.GetAll();
    foreach (var car in cars)
    {
        Console.WriteLine(car);
    }
}

static void AddNewCar(IRepository<Car> newCar)
{
    Console.Write("Podaj markę samochodu: ");
    var brand = Console.ReadLine();
    Console.Write("Podaj model samochodu: ");
    var model = Console.ReadLine();
    var car = new Car(brand, model);

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

void WriteToFileTxt(FileRepository<Car> carRepository)
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
