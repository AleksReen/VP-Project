using System.Linq;
using System.Collections.Generic;

namespace VPproject.Classes
{
    class ListRemontsProducts:List<Товар>
    {
        public ListRemontsProducts()
        {
            this.AddRange(Remonts.dbContext.Товар.OrderBy(x => x.Наименование_товара).ToList());           
        }
    }
}
