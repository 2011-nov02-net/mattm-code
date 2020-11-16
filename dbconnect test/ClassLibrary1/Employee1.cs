using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary1
{
    public partial class Employee1
    {
        public Employee1()
        {
            EmpDetails = new HashSet<EmpDetail>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Ssn { get; set; }
        public int DeptId { get; set; }

        public virtual Department Dept { get; set; }
        public virtual ICollection<EmpDetail> EmpDetails { get; set; }
    }
}
