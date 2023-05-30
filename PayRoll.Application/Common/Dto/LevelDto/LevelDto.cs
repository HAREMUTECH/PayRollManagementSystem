using PayRoll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Common.Dto.LevelDto
{
    public class LevelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PositionId { get; set; }
        public string PositionName { get; set; }
    }
}
