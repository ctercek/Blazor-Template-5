using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace btp.Data.Models
{
    public class MigrationRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
