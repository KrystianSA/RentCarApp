using RentCarApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApp.Components.Models
{
    public class CarsCatalog : EntityBase
    {
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}
