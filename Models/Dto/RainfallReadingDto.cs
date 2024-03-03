namespace Models.Dto
{
    public class RainfallReadingDto
    {
        public DateTime DateMeasured { get; set; }
        public decimal AmountMeasured { get; set; }
        public IEnumerable<Items> Items { get; set; }
    }

    public class Items
    {
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }
    }

}
