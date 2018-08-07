using System.Linq;
using System.Collections.Generic;

namespace VPproject.Classes
{
    class ListPostPost: List<Поставщик>
    {
        public ListPostPost()
        {
            this.AddRange(Products.dbContext.Поставщик.OrderBy(x => x.Наименование).ToList());        
        }
    }
}
