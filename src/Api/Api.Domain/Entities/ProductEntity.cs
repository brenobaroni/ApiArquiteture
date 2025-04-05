using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Domain.Entities
{
    public class Product : BaseData
    {
        public int name { get; set; }
        public float price { get; set; }

        public virtual ICollection<SaleItem> sale_items { get; set; } = new List<SaleItem>();
    }
}
