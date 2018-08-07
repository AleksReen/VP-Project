using System.Linq;
using System.Collections.Generic;

namespace VPproject.Classes
{
    class ListPostProd : List <Поставщик>
    {
        public ListPostProd()
        {
            this.AddRange(Products.dbContext.Поставщик.OrderBy(x => x.Наименование).ToList());            
        }
    }
}
