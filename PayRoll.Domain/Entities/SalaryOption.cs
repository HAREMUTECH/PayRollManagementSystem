using PayRoll.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Domain.Entities
{
    public class SalaryOption : BaseEntity
    {
        public string SalaryOptionName { get; set; }
        public decimal Amount { get; set; }
        public SalaryType SalaryType { get; set; }
        public Guid CadreId { get;set; }
        public Cadre Cadre { get; set; }
    }
}
