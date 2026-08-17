using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductOptionValue : BaseAuditableEntity
    {
        public long ProductOptionId { get; set; }

        public ProductOption ProductOption { get; set; } = null!;

        public string Value { get; set; } = string.Empty;
        public ICollection<ProductVariantOptionValue> Variants { get; set; } = [];
    }
}
