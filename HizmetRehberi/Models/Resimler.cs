//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HizmetRehberi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resimler
    {
        public int ResimID { get; set; }
        public string ResimYolu { get; set; }
        public int FirmaID { get; set; }
    
        public virtual Firmalar Firmalar { get; set; }
    }
}
