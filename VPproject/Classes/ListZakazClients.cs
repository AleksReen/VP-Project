using System.Linq;
using System.Collections.Generic;

namespace VPproject.Classes
{
    class ListZakazClients: List<Клиент>
    {
        public ListZakazClients()
        {
            this.AddRange(Orders.dbContext.Клиент.OrderBy(x => x.Организация).ToList());           
        }
    }
}
