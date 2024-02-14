using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class PopupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public bool IsGeneral { get; set; }
        public bool IsEmailOrGroup { get; set; }
        public Nullable<int> SZPopupGroupId { get; set; }

        public string GroupId { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
    }

    public class PopupGroupModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string UserId { get; set; }
    }
}