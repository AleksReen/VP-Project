using System.Linq;
using System.Collections.ObjectModel;

namespace VPproject.Classes
{
    class ListZakazProduct: ObservableCollection<Товар>
    {
        public ListZakazProduct()
        {
            var listZakPr = Orders.dbContext.Товар;

            var queryZakPr = from pr in listZakPr
                             orderby pr.Наименование_товара
                             select pr;

            foreach (var pr in listZakPr)
            {
                this.Add(pr);
            }
        }
    }
}
