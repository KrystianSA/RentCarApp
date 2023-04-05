using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApp.Entities
{
    public class Cars : EntityBase
    {
        public override string ToString() => $"{Id}. {carBrand} {carModel}";
    }
}
