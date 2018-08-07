using System.Linq;
using System.Collections.Generic;

namespace VPproject.Classes
{
    class ListZakazEmployee: List<Сотрудник>
    {
        public ListZakazEmployee()
        {
            this.AddRange(Orders.dbContext.Сотрудник.OrderBy(x => x.Фамилия).ToList());         
        }
    }
}
