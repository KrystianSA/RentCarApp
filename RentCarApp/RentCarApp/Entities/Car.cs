using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarApp.Entities
{
    public class Car : EntityBase
    {
        public Car() 
        {
        
        }

        public Car(string brand, string model)
        {
            Brand = brand;
            Model = model;
        }

        public string Brand { get; set; }
        public string Model { get; set; }
        public bool IsRented { get; set; }
        public bool IsReturned { get; set; }    

        public string Rented 
        {
            get 
            {
                return IsRented ? "Wypożyczony" : "";
            }
        }

        public string Returned
        {
            get
            {
                return IsReturned ? "Zwrócony" : "";
            }
        }

        public override string ToString() => $"{Id}. {Brand} {Model} {Rented} {Returned}";
    }
}
