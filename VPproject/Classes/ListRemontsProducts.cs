using System.Linq;
using System.Collections.ObjectModel;

namespace VPproject.Classes
{
    class ListRemontsProducts:ObservableCollection<Товар>
    {
        public ListRemontsProducts()
        {

            var listRemProd = Remonts.dbContext.Товар;
            var queryRemProd = from rp in listRemProd
                               orderby rp.Наименование_товара
                               select rp ;

            foreach (var rp in queryRemProd)
            {
                Add(rp);
            }
        }
    }
}
