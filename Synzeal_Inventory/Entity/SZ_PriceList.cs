//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Synzeal_Inventory.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SZ_PriceList
    {
        public int Id { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string TenPrice { get; set; }
        public string TwentyFivePrice { get; set; }
        public string FiftyPrice { get; set; }
        public string HundredPrice { get; set; }
        public string TwoHundredPrice { get; set; }
        public string DiscountUSD { get; set; }
        public string LeadTime { get; set; }
        public string FivehundredPrice { get; set; }
        public string OneThousandPrice { get; set; }
        public string TenUSD { get; set; }
        public string TwentyfiveUSD { get; set; }
        public string FiftyUSD { get; set; }
        public string OnehundredUSD { get; set; }
        public string TwohundredFiftyUSD { get; set; }
        public string FivehundredUSD { get; set; }
        public string OneThousandUSD { get; set; }
        public Nullable<int> CategoryMasterUSdId { get; set; }
        public Nullable<bool> IsUsd { get; set; }
        public Nullable<int> CategoryMasterINRId { get; set; }
        public string DiscountINR { get; set; }
        public string ProductRemark { get; set; }
        public Nullable<bool> IsPriceApproved { get; set; }
    
        public virtual Product Product { get; set; }
    }
}