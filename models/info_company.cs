//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace сайт_магазина_СП.models
{
    using System;
    using System.Collections.Generic;
    
    public partial class info_company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public info_company()
        {
            this.products = new HashSet<product>();
        }
    
        public string ID_фирма_изготовитель { get; set; }
        public string название_фирмы { get; set; }
        public string фамилия_директора { get; set; }
        public string имя_директора { get; set; }
        public string отчество_директора { get; set; }
        public string электронная_почта_фирмы { get; set; }
        public string номер_телефона_фирмы { get; set; }
        public string индекс_юр_адреса { get; set; }
        public string город_юр_адреса { get; set; }
        public string улица_юр_адреса { get; set; }
        public string дом_юр_адреса { get; set; }
        public string помещение_юр_адреса { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}
