using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductImage : BaseAuditableEntity
    {
        public long ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public long? ProductVariantId { get; set; }

        public ProductVariant? ProductVariant { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        public bool IsPrimary { get; set; }
    }
}
