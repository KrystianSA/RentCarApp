using RentCarApp.Components.CvsReader.Models;

namespace RentCarApp.Components.CvsReader.Extensions
{
    public static class ManufacturerExtension
    {
        public static IEnumerable<Manufacturer> ToManufacturer(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');
                yield return new Manufacturer
                {
                    Name = columns[0],
                    Country = columns[1],
                    Year = int.Parse(columns[2])
                };
            }
        }
    }
}
