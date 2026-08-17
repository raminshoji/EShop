using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductVariant : BaseAuditableEntity
    {
        public long ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public string SKU { get; set; } = string.Empty;
        public string VariantKey { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public byte[] RowVersion { get; set; } = [];
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;
       
        public ICollection<ProductVariantOptionValue> OptionValues { get; set; } = [];
        public ICollection<ProductImage> Images { get; set; } = [];
    }
}
