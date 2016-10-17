using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.ViewModels
{
    public class BD03ViewModel_SM01DDL
    {
        public string user_org { get; set; }
        public string user_dept { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
        public bool usable { get; set; }
        public string user_mail { get; set; }
        public string user_tel { get; set; }



    }
    public class BD03AddModel
    {
        public string user_org { get; set; }
        public string user_dept { get; set; }
        public string user_id { get; set; }
        public string user_pwd { get; set; }
        public string user_name { get; set; }
        public string user_sex { get; set; }
        public string user_mail { get; set; }
        public string user_tel { get; set; }
        public int auth_type { get; set; }
        public bool usable { get; set; }
        //public string maker_id { get; set; }
    }
}
