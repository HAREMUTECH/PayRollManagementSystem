using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Domain.Entities
{
    public class PayRollManagement: BaseEntity
    {
        public Guid EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal NetPay { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal IncomeAmount { get; set; }
    }
}
