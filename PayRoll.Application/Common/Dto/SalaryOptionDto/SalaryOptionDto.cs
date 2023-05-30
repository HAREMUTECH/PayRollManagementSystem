using PayRoll.Domain.Entities;
using PayRoll.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Common.Dto.SalaryOptionDto
{
    public class SalaryOptionDto
    {
        public Guid Id { get; set; }    
        public string SalaryOptionName { get; set; }
        public decimal Amount { get; set; }
        public SalaryType SalaryType { get; set; }
        public Guid CadreId { get; set; }
        public string CadreName { get; set; }
    }
}
