using System.Linq;
using System.Collections.Generic;

namespace VPproject.Classes
{
    class ListRemontsClients : List<Клиент>
    {
        public ListRemontsClients()
        {                
            this.AddRange(Remonts.dbContext.Клиент.OrderBy(x => x.Организация).ToList());         
        }
    }
}
