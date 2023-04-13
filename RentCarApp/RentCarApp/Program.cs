using Microsoft.Extensions.DependencyInjection;
using RentCarApp.Repositories;
using RentCarApp.Entities;
using RentCarApp.Services;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEventHandlerService, EventHandlerService>();
services.AddSingleton<IDataProvider, DataProvider>();
services.AddSingleton<IDataSelector, DataSelector>();
services.AddSingleton<IRepository<Car>, FileRepository<Car>>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();
