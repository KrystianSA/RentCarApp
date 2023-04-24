namespace RentCarApp.Entities
{
    public class Car : EntityBase
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public decimal Power { get; set; }
        public decimal PriceForDay { get; set; }
        public bool IsReadyToRent { get; set; }
        public bool IsRented { get; set; }
        public bool IsReturned { get; set; }

        public string ReadyToRent
        {
            get
            {
                return IsReadyToRent ? "Do wypożyczenia" : "";
            }
        }

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

        public override string ToString() => $"{Id}. {Brand} {Model},\n" +
                                             $" \n   Kolor : {Color}" +
                                             $" \n   Cena za dobę : {PriceForDay} zł" +
                                             $" \n   Moc : {Power} KM" +
                                             $"                   {ReadyToRent} " +
                                             $"                   {Rented} " +
                                             $"                   {Returned} ";
    }
}