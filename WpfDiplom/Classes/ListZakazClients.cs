using System.Linq;
using System.Collections.ObjectModel;

namespace WpfDiplom.Classes
{
    class ListZakazClients: ObservableCollection<Клиент>
    {
        public ListZakazClients()
        {
            var listZakCl = Orders.DataEntitiesOrders.Клиент;
                


            var queryZakCl = from cl in listZakCl
                             orderby cl.Организация
                             select cl;

            foreach (var cl in queryZakCl)
            {
                this.Add(cl);
            }
        }
    }
}
