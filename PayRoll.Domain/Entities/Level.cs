using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Domain.Entities
{
    public class Level : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PositionId { get; set; }
        public Position Position { get; set; }

    }
}
