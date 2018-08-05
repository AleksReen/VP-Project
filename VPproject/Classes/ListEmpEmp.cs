using System.Linq;
using System.Collections.ObjectModel;

namespace VPproject.Classes
{
    class ListEmpEmp: ObservableCollection<Сотрудник>
    {
        public ListEmpEmp()
        {
            var listEmp = Employees.DataEntitiesEmployees.Сотрудник;



            var queryEmp = from emp in listEmp
                           orderby emp.Фамилия
                             select emp;

            foreach (var emp in queryEmp)
            {
                this.Add(emp);
            }
        }
    }
}
