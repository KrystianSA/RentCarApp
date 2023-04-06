using RentCarApp.Entities;
using RentCarApp.Repositories;
using RentCarApp.Entities;
using RentCarApp.Repositories;

Console.WriteLine("Witaj w aplikacji do zarządzania wypożyczalnią samochodów");
Console.WriteLine("---------------------------------------------------------");
Console.WriteLine(" ");
Console.WriteLine("1. Samochody do wypożyczenia.");
Console.WriteLine("2. Samochody zwrócone.");
Console.WriteLine(" ");
Console.Write("Wybierz Id opcji: ");
var choiceForList = Console.ReadLine();

var carsForRent = new FileRepository<CarsForRent>();
carsForRent.DeleteFileTxt("Historia.txt");

carsForRent.ItemAdded += CarsForRentItemAdded;
carsForRent.ItemRemoved += CarsForRentItemRemoved;
static void CarsForRentItemAdded(object sender, CarsForRent e)
{
    using (var writer = File.AppendText("Historia.txt"))
    {
        writer.WriteLine($"[{DateTime.Now}] - Car Added - {e.carBrand} {e.carModel}");
    }
}

static void CarsForRentItemRemoved(object sender, CarsForRent e)
{
    using (var writer = File.AppendText("Historia.txt"))
    {
        writer.WriteLine($"[{DateTime.Now}] - Car Removed - {e.carBrand} {e.carModel}");
    }
}

static void CarsForRent(IRepository<CarsForRent> carsForRent)
{
    while (true)
    {
        Console.WriteLine(" ");
        Console.WriteLine("1. Dodaj samochód do list.");
        Console.WriteLine("2. Usuń samochód z listy.");
        Console.WriteLine("3. Wyświetl listę samochodów.");
        Console.WriteLine("4. Zapisz zmiany do osobnego pliku.");
        Console.WriteLine(" ");
        Console.Write("Wybierz Id czynności : ");
        var choiceOfActivity = Console.ReadLine();

        if (choiceOfActivity == "1")
        {
            Console.Write($"Marka : ");
            var carBrand = Console.ReadLine();
            Console.Write("Model : ");
            var carModel = Console.ReadLine();
            carsForRent.Add(new CarsForRent { carBrand = $"{carBrand}", carModel = $"{carModel}" });
            carsForRent.WriteToFileJson();
            carsForRent.ReadFromFileJsonAndAddToUpdatedList();
        }
        else if (choiceOfActivity == "2")
        {
            Console.Write("Wybierz ID samochodu do usunięcia : ");
            var carId = Console.ReadLine();
            carsForRent.Remove(carsForRent.GetById(int.Parse(carId)));
            carsForRent.WriteToFileJson();
            carsForRent.ReadFromFileJsonAndAddToUpdatedList();
        }
        else if (choiceOfActivity == "3")
        {
            foreach (var car in carsForRent.GetAll())
            {
                Console.WriteLine($"ID: {car.Id} Marka: {car.carBrand} Model: {car.carModel}");
            }
        }
        else if (choiceOfActivity == "4")
        {
            carsForRent.DeleteFileTxt("Samochody do wynajmu.txt");
            carsForRent.SaveToFileTxt("Samochody do wynajmu.txt");
        }
        else
        {
            break;
        }
    }
}

var returnedCars = new FileRepository<ReturnedCars>();
static void ReturnedCars(IRepository<ReturnedCars> returnedCars)
{
    while (true)
    {
        Console.WriteLine(" ");
        Console.WriteLine("1. Dodaj samochód do list.");
        Console.WriteLine("2. Usuń samochód z listy.");
        Console.WriteLine("3. Wyświetl listę samochodów.");
        Console.WriteLine("4. Zapisz zmiany do osobnego pliku.");
        Console.WriteLine(" ");
        Console.Write("Wybierz Id czynności : ");
        var choiceOfActivity = Console.ReadLine();

        if (choiceOfActivity == "1")
        {
            Console.Write($"Marka : ");
            var carBrand = Console.ReadLine();
            Console.Write("Model : ");
            var carModel = Console.ReadLine();
            returnedCars.Add(new ReturnedCars { carBrand = $"{carBrand}", carModel = $"{carModel}" });
            returnedCars.WriteToFileJson();
            returnedCars.ReadFromFileJsonAndAddToUpdatedList();
        }
        else if (choiceOfActivity == "2")
        {
            Console.Write("Wybierz ID samochodu do usunięcia : ");
            var carId = Console.ReadLine();
            returnedCars.Remove(returnedCars.GetById(int.Parse(carId)));
            returnedCars.WriteToFileJson();
            returnedCars.ReadFromFileJsonAndAddToUpdatedList();
        }
        else if (choiceOfActivity == "3")
        {
            foreach (var car in returnedCars.GetAll())
            {
                Console.WriteLine($"ID: {car.Id} Marka: {car.carBrand} Model: {car.carModel}");
            }
        }
        else if (choiceOfActivity == "4")
        {
            returnedCars.DeleteFileTxt("Zwrócone samochody.txt");
            returnedCars.SaveToFileTxt("Zwrócone samochody.txt");
        }
        else if (choiceOfActivity == "q")
        {
            break;
        }
    }
}

while (true)
{
    switch (choiceForList) 
    {
        case "1":
            CarsForRent(carsForRent);
            break;
        case "2":
            ReturnedCars(returnedCars);
            break;
        default:
            break;
    }
}
