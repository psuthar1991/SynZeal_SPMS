using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class UserManagementModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string CompanyIds { get; set; }
        public string CompanyNames { get; set; }
        public bool IsNotification { get; set; }
        public bool IsPaymentShow { get; set; }
        public bool isNewsLetter { get; set; }

        public Nullable<int> DefaultCurrencyId { get; set; }
    }
}