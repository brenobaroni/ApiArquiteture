namespace Api.Domain.Entities
{
    public class Sale : BaseData
    {
        public string sale_number { get; set; }
        public DateTime sale_date { get; set; }
        public Guid customer_id { get; set; }
        public Guid branch_id { get; set; }
        public decimal total_amount { get; set; }
        public bool cancelled { get; set; }

        public virtual ICollection<SaleItem> items { get; set; } = new List<SaleItem>();
    }

    public class SaleItem : BaseData
    {
        public Guid sale_id { get; set; }
        public Guid product_id { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
        public decimal discount { get; set; }
        public decimal total { get; set; }
        public bool is_cancelled { get; set; }

        public virtual Sale sale { get; set; }
        public virtual Product product { get; set; }
    }
}