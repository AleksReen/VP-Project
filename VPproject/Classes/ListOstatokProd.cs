using System.Linq;
using System.Collections.Generic;

namespace VPproject.Classes
{
    class ListOstatokProd : List<Товар>
    {
        public ListOstatokProd()
        {
            this.AddRange(Products.dbContext.Товар.OrderBy(x => x.Наименование_товара).ToList());     
        }
    }
}
