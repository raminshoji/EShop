using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public long CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public ICollection<ProductVariant> Variants { get; set; } = [];

        public ICollection<ProductImage> Images { get; set; } = [];

        public ICollection<ProductOptionAssignment> OptionAssignments { get; set; } = [];
    }
}
