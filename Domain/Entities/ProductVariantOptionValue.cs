using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductVariantOptionValue : BaseAuditableEntity
    {
        public long ProductVariantId { get; set; }

        public ProductVariant ProductVariant { get; set; } = null!;

        public long ProductOptionValueId { get; set; }

        public ProductOptionValue ProductOptionValue { get; set; } = null!;
    }
}

