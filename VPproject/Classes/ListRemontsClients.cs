using System.Linq;
using System.Collections.ObjectModel;

namespace VPproject.Classes
{
    class ListRemontsClients : ObservableCollection<Клиент>
    {
        public ListRemontsClients()
        {       
            var listRemCl = Remonts.DataEntitiesRemonts.Клиент;
            

            var queryRemCl = from rс in listRemCl
                             orderby rс.Организация
                             select rс;

            foreach (var rс in queryRemCl)
            {
                this.Add(rс);
            }
        }
    }
}
