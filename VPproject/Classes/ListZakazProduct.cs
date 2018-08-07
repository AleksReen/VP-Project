using System.Linq;
using System.Collections.Generic;

namespace VPproject.Classes
{
    class ListZakazProduct: List<Товар>
    {
        public ListZakazProduct()
        {
            this.AddRange(Orders.dbContext.Товар.OrderBy(x => x.Наименование_товара).ToList());           
        }
    }
}
