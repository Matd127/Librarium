//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Librarium
{
    using System;
    using System.Collections.Generic;
    
    public partial class fines
    {
        public int fineId { get; set; }
        public int readerId { get; set; }
        public string type_of_fine { get; set; }
        public int cost { get; set; }
        public bool isPaid_ { get; set; }
    
        public virtual readers readers { get; set; }
    }
}
