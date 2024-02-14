using Synzeal_Inventory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models.Chat
{
    public class UserNotificationList
    {
        public string NotificationType { get; set; }
        public int NotificationID { get; set; }
        public Customer User { get; set; }
        public DateTime CreatedOn { get; set; }
        public string NotificationStatus { get; set; }
        public int TotalNotifications { get; set; }
    }
}