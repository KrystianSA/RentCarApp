using RentCarApp.Components.CvsReader;
using System.Xml.Linq;

namespace RentCarApp.Components.XmlCreator
{
    public class XmlCreator : IXmlCreator
    {
        private readonly ICsvReader _csvReader;

        public XmlCreator(ICsvReader csvReader)
        {
            _csvReader = csvReader;
        }
        public void CreateXmlFile()
        {
            var cars = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");
            var manufacturers = _csvReader.ProcessManufacturers("Resources\\Files\\manufacturers.csv");

            var carGroups = manufacturers.GroupJoin(
                cars,
                x => x.Name,
                x => x.Manufacturer,
                (m, g) =>
                new
                {
                    Manufacturer = m,
                    Cars = g
                });

            var document = new XDocument();

            var fileXml = new XElement("Manufacturers", carGroups
                .Select(x =>
                     new XElement("Manufacturer",
                        new XAttribute("Name", x.Manufacturer.Name),
                        new XAttribute("Country", x.Manufacturer.Country),
                           new XElement("Cars",
                              new XAttribute("Country", x.Manufacturer.Country),
                              new XAttribute("CombinedSum", x.Cars.Sum(x => x.Combined)),
                                 new XElement("Car", x.Cars
                                 .Select(x =>
                                 new XElement("Car",
                                    new XAttribute("Model", x.Model),
                                    new XAttribute("Combined", x.Combined))).Distinct())))));
            document.Add(fileXml);
            document.Save("Katalog Samochodów.xml");
        }
    }
}