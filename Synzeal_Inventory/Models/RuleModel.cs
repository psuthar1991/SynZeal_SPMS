using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Synzeal_Inventory.Models
{
    public class RuleModel
    {
        public int id { get; set; }
        public string ruleName { get; set; }
        public string ruletag { get; set; }
        public int condition { get; set; }
        public List<RulesData> rules { get; set; }
    }

    public class RulesData
    {
        public int ruledetailid { get; set; }
        public int typedata { get; set; }
        public int query { get; set; }
        public string value { get; set; }
    }
}