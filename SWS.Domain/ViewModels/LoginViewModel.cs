using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.ViewModels
{
    public class LoginViewModel
    {
        [DisplayFormat(ConvertEmptyStringToNull= false)]
        [Display(Name="帳號")]
        [Required(ErrorMessage = "請輸入帳號！")]
        public string user_id { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "請輸入密碼！")]
        public string user_password { get; set; }
        public string user_name { get; set; }
        public string usable { get; set; }
        public string user_org { get; set; }
        public string user_dept { get; set; }
    }
}
