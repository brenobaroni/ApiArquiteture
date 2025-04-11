namespace Api.Domain.Entities
{
    public class Product : BaseData
    {
        public string title { get; set; }
        public decimal price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }

        public virtual ICollection<SaleItem> sale_items { get; set; } = new List<SaleItem>();
    }
}
