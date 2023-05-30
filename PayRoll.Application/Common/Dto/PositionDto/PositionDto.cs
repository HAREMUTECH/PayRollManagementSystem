using PayRoll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Common.Dto.PositionDto
{
    public class PositionDto
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
       
    }
}
