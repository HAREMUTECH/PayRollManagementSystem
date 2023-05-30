using PayRoll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Common.Dto.LevelDto
{
    public class LevelRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PositionId { get; set; }
    }
}
