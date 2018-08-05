using System.Linq;
using System.Collections.ObjectModel;

namespace VPproject.Classes
{
    class ListOstatokProd : ObservableCollection<Товар>
    {
        public ListOstatokProd()
        {

            var listPP = Products.dbContext.Товар;

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
