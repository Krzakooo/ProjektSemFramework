//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjektSemFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tlumacze
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tlumacze()
        {
            this.Dokumenties = new HashSet<Dokumenty>();
            this.Jezyki_Tlumacza = new HashSet<Jezyki_Tlumacza>();
        }
    
        public int id_tlumacza { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string telefon { get; set; }
        public string e_mail { get; set; }
        public string jezyk_ojczysty { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dokumenty> Dokumenties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jezyki_Tlumacza> Jezyki_Tlumacza { get; set; }
    }
}
