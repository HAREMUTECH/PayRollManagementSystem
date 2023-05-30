using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Domain.Entities
{
    public class Cadre : BaseEntity
    {
        public string CaderName { get; set; }
        public string Description { get; set; }
        public Level Level { get; set; }
        public Guid LevelId { get; set; }

    }
}
