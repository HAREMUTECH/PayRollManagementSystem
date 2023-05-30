using PayRoll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Common.Dto.CadreDto
{
    public class CadreDto
    {
        public Guid Id { get; set; }
        public string CaderName { get; set; }
        public string Description { get; set; }
        public Guid LevelId { get; set; }
        public string LevelName { get; set; }
    }
}
