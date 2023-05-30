using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Common.Dto.CadreDto
{
    public class CadreRequestModel
    {
        public string CaderName { get; set; }
        public string Description { get; set; }
        public Guid LevelId { get; set; }
    }
}
