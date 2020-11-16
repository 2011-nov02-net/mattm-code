using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary1
{
    public partial class EmpDetail
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal Salary { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual Employee1 Employee { get; set; }
    }
}
