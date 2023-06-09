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
    using System.IO;
    
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.zakazs = new HashSet<zakaz>();
        }
        public string GetPhoto
        {
            get
            {
                if (фото_товара is null)
                    return Directory.GetCurrentDirectory() + @"\Images\picture.png";
                return Directory.GetCurrentDirectory() + @"\Images\" + фото_товара.Trim();
            }
        }
        public int ID_товара { get; set; }
        public string наименование { get; set; }
        public string тип_товара { get; set; }
        public string срок_годности { get; set; }
        public string состав { get; set; }
        public int количество { get; set; }
        public string КЖБУ { get; set; }
        public string страна_производства { get; set; }
        public string фото_товара { get; set; }
        public int артикул { get; set; }
        public string фирма_изготовитель { get; set; }
        public int вес { get; set; }
        public int цена { get; set; }
    
        public virtual info_company info_company { get; set; }
        public virtual product_types product_types { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<zakaz> zakazs { get; set; }
    }
}
