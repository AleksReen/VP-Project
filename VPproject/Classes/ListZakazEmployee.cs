using System.Linq;
using System.Collections.ObjectModel;

namespace VPproject.Classes
{
    class ListZakazEmployee: ObservableCollection<Сотрудник>
    {
        public ListZakazEmployee()
        {
            var listZakEm = Orders.DataEntitiesOrders.Сотрудник;
            
            var queryZakEm = from em in listZakEm
                             orderby em.Фамилия
                             select em;

            foreach (var em in queryZakEm)
            {
                this.Add(em);
            }
        }
    }
}
