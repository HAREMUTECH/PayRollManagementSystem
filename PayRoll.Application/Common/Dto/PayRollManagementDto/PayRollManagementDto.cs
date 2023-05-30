using PayRoll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Common.Dto.PayRollDto
{
    public class PayRollManagementDto
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal NetPay { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal IncomeAmount { get; set; }
    }
}
