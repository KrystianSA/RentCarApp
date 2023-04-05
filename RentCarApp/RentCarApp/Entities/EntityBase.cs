using RentCarApp.Entities;

namespace RentCarApp.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }
        public string carBrand { get; set; }
        public string carModel { get; set; }
    }
}