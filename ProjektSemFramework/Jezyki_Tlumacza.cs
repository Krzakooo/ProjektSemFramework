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
    
    public partial class Jezyki_Tlumacza
    {
        public int id_jezyki_tlumacza { get; set; }
        public int id_jezyka { get; set; }
        public int id_tlumacza { get; set; }
    
        public virtual Jezyki Jezyki { get; set; }
        public virtual Tlumacze Tlumacze { get; set; }
    }
}
