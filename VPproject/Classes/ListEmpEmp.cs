using System.Linq;
using System.Collections.Generic;

namespace VPproject.Classes
{
    class ListEmpEmp: List<Сотрудник>
    {
        public ListEmpEmp()
        {
            this.AddRange(Employees.dbContext.Сотрудник.OrderBy(x => x.Фамилия).ToList());          
        }
    }
}
