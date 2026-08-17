using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductOption : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<ProductOptionValue> Values { get; set; } = [];
        public ICollection<ProductOptionAssignment> ProductAssignments { get; set; } = [];
    }
}
