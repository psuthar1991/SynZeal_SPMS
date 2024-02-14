using Synzeal_Inventory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models.Chat
{
    public class MessageRecords
    {
        public List<ChatMessage> Messages { get; set; }
        public int TotalMessages { get; set; }
        public int LastChatMessageId { get; set; }
    }
}