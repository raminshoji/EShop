using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductOptionAssignment : BaseEntity
    {
        public long ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public long ProductOptionId { get; set; }

        public ProductOption ProductOption { get; set; } = null!;
    }
}
