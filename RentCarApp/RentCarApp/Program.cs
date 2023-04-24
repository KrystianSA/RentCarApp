using Microsoft.Extensions.DependencyInjection;
using RentCarApp.Repositories;
using RentCarApp.Entities;
using RentCarApp.Services;
using RentCarApp.Components.CvsReader;
using RentCarApp.Components.XmlCreator;
using RentCarApp.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RentCarApp.Mapping;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEventHandlerService, EventHandlerService>();
services.AddSingleton<ICarDataProvider, CarDataProvider>();
services.AddSingleton<ICarDataSelector, CarDataSelector>();
//services.AddSingleton<IRepository<Car>, FileRepository<Car>>();
services.AddSingleton<IRepository<Car>, SqlRepository<Car>>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlCreator, XmlCreator>();
services.AddSingleton<DbContext,RentCarAppDbContext>();
services.AddDbContext<RentCarAppDbContext>(options =>
    options.UseSqlServer("Data Source=LAPTOP-FGS0SEO7\\SQLEXPRESS01;Encrypt=false;Database=Storage;Integrated Security=True"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();
