using System.Linq;
using System.Collections.ObjectModel;

namespace WpfDiplom.Classes
{
    class ListPostProd : ObservableCollection <Поставщик>
    {
        public ListPostProd()
        {
            var listPP = Products.DataEntitiesProducts.Поставщик;

            var queryPP = from rс in listPP
                          orderby rс.Наименование
                          select rс;

            foreach (var rс in queryPP)
            {

                Add(rс);
            }
        }
    }
}
