using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.ViewModels
{
    public class BD02ViewModel_SM01DDL
    {
        public string org_id { get; set; }
        public string org_name { get; set; }
        public string dept_year { get; set; }
        public string dept_org { get; set; }
        public string dept_id { get; set; }
        public string dept_name { get; set; }
        public string dept_memo { get; set; }
        //public string dept_name { get; set; }
        //public string make_date { get; set; }
    }
    public class BD02AddModel
    {
        public string dept_year { get; set; }
        public string dept_org { get; set; }
        public string dept_id { get; set; }
        public string dept_name { get; set; }
        public string dept_memo { get; set; }
        public System.DateTime make_date { get; set; }

    }
}
