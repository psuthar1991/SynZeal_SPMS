using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class ConversationMessageModel
    {
        public class Author
        {
            public string id { get; set; }
            public bool me { get; set; }
            public string email { get; set; }
            public string name { get; set; }
            public string avatar_url { get; set; }
        }

        public class CcField
        {
            public string name { get; set; }
            public string address { get; set; }
        }

        public class FromField
        {
            public string name { get; set; }
            public string address { get; set; }
        }

        public class Message
        {
            public string id { get; set; }
            public string subject { get; set; }
            public string preview { get; set; }
            public string type { get; set; }
            public int delivered_at { get; set; }
            public int updated_at { get; set; }
            public int created_at { get; set; }
            public string email_message_id { get; set; }
            public List<object> in_reply_to { get; set; }
            public List<string> references { get; set; }
            public FromField from_field { get; set; }
            public List<ToField> to_fields { get; set; }
            public List<CcField> cc_fields { get; set; }
            public List<object> bcc_fields { get; set; }
            public List<object> reply_to_fields { get; set; }
            public List<object> attachments { get; set; }
            public Author author { get; set; }
        }

        public class Root
        {
            public List<Message> messages { get; set; }
        }

        public class ToField
        {
            public string name { get; set; }
            public string address { get; set; }
        }


        public class RootDraft
        {
            public Draft drafts { get; set; }
        }

        public class Draft
        {
            public string id { get; set; }
            public string conversation { get; set; }
            public bool send { get; set; }
            public string subject { get; set; }
            public string body { get; set; }
            public List<ToField> to_fields { get; set; }
            public List<CcField> cc_fields { get; set; }
            public List<object> bcc_fields { get; set; }
            public FromField from_field { get; set; }
            public List<AttachmentDataModel> attachments { get; set; }
            public string message_id { get; set; }
            public bool quote_previous_message { get; set; }
            public List<string> references { get; set; }
        }

        public class AttachmentDataModel
        {
            public string base64_data { get; set; }
            public string filename { get; set; }
        }

    }
}