//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetInvenApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class wired_devices
    {
        public short asset_id { get; set; }
        public string switch_port { get; set; }
        public Nullable<short> vlan_id { get; set; }
        public string switch_name { get; set; }
    
        public virtual device device { get; set; }
        public virtual vlan vlan { get; set; }
    }
}