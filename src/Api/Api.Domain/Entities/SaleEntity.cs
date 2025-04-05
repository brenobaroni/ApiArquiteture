namespace Api.Domain.Entities
{
    public class Sale: BaseData
    {
        public virtual ICollection<SaleItem> sale_items { get; set; } = new HashSet<SaleItem>()!; 
    }


    public class SaleItem : Base
    {
        public int product_id { get; set; }
        public int sale_id { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }

        public virtual Sale sale { get; set; } = default!;
        public virtual Product product { get; set; } = default!;
    }
}
