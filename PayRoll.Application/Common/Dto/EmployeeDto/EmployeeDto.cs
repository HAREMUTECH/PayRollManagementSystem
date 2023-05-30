using PayRoll.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRoll.Application.Common.Dto.EmployeeDto
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public Guid CadreId { get; set; }
        public string CadreName { get; set; }
        public string Level { get; set; }
        public string Position { get; set; }
    }
}
