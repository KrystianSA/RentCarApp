using Microsoft.Extensions.DependencyInjection;
using RentCarApp.Repositories;
using RentCarApp.Entities;
using RentCarApp.Services;
using RentCarApp.Components.CvsReader;
using RentCarApp.Components.XmlCreator;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEventHandlerService, EventHandlerService>();
services.AddSingleton<ICarDataProvider, CarDataProvider>();
services.AddSingleton<ICarDataSelector, CarDataSelector>();
services.AddSingleton<IRepository<Car>, FileRepository<Car>>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlCreator, XmlCreator>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();
