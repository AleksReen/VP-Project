using System.Linq;
using System.Collections.ObjectModel;

namespace WpfDiplom.Classes
{
    class ListPostPost: ObservableCollection<Поставщик>
    {
        public ListPostPost()
        {
            var listPP = Providers.DataEntitiesProviders.Поставщик;

            var queryPP = from p in listPP
                          orderby p.Наименование
                          select p;

            foreach (var p in queryPP)
            {
                Add(p);
            }
        }
    }
}
