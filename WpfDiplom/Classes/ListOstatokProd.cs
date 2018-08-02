using System.Linq;
using System.Collections.ObjectModel;

namespace WpfDiplom.Classes
{
    class ListOstatokProd : ObservableCollection<Товар>
    {
        public ListOstatokProd()
        {

            var listPP = Products.DataEntitiesProducts.Товар;

            var queryPP = from rс in listPP
                          orderby rс.Наименование_товара
                          select rс;

            foreach (var rс in queryPP)
            {
                Add(rс);
            }
        }
    }
}
