using System.Linq;
using System.Collections.ObjectModel;

namespace VPproject.Classes
{
    class ListZakazClients: ObservableCollection<Клиент>
    {
        public ListZakazClients()
        {
            var listZakCl = Orders.dbContext.Клиент;
                


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
