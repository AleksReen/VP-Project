//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VPproject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Поставщик
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Поставщик()
        {
            this.Товар = new HashSet<Товар>();
        }
    
        public int Код_поставщика { get; set; }
        public string Наименование { get; set; }
        public string Адрес { get; set; }
        public string Телефон { get; set; }
        public string Контактное_лицо { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Товар> Товар { get; set; }
    }
}
