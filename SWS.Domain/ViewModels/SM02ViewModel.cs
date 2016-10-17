using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.ViewModels
{
    public class SM02ViewModel
    {
        public string year { get; set; }
        public string org { get; set; }
        public string dept { get; set; }
        public string soft_id { get; set; }
        public int detail_id { get; set; }
        public string keep_org { get; set; }
        public string keep_man { get; set; }
        public string use_org { get; set; }
        public string use_man { get; set; }
        public string soft_ver { get; set; }
        public Nullable<int> soft_cost { get; set; }
        public Nullable<int> auth_number { get; set; }
        public System.DateTime update_date { get; set; }
        public string decrease_reason { get; set; }
        public string decrease_handle { get; set; }
        public string detail_memo { get; set; }
    }

}
