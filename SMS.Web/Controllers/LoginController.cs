using SMS.BLL.Services;
using SMS.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SMS.Domain;

namespace SMS.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        //[HttpPost, ActionName("UserLogin")]
        [AcceptVerbs(HttpVerbs.Post)]
        [AllowAnonymous]
        public ActionResult UserLogin(LoginViewModel model)
        {
            // 實務上可能驗證表單.驗證該使用者是否存在.寫在BLL層.Web層只要知道是否登入成功
            LoginService LS = new LoginService();
            bd03_user UserData = LS.CheckUser(model);
            // 登入成功，寫入FormsAuthentication
            if (UserData.user_id != null)
            {
                SetLogin(UserData, "Admin");
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("Index", "Login");
        }

        /// <summary>寫入FormsAuthentication登入資訊</summary>
        /// <param name="UserID"></param>
        /// <param name="Role"></param>
        private void SetLogin(bd03_user UserData, string Role)
        {
            Session.RemoveAll();
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                UserData.user_name,
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,//將管理者登入的 Cookie 設定成 Session Cookie
                UserData.user_id + ","+ UserData.user_org+","+UserData.user_dept+","+UserData.auth_type.ToString(),//ID,org,dept,authtype       
                FormsAuthentication.FormsCookiePath);//你想要存放在 User.Identy.Name 的值，通常是使用者帳號
            string encTicket = FormsAuthentication.Encrypt(ticket);
            //FormsAuthentication.SetAuthCookie(UserData.user_name, false);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            System.Web.Security.FormsAuthentication.RedirectFromLoginPage(UserData.user_name, true);
            Session["UserID"] = UserData.user_id;
            Session["UserAuthType"] = UserData.auth_type;
            Session["UserOrg"] = UserData.user_org;
            Session["UserDept"] = UserData.user_dept;      
        }

        /// <summary>登出頁面</summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            //清除所有的 session
            Session.RemoveAll();

            //建立一個同名的 Cookie 來覆蓋原本的 Cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            //建立 ASP.NET 的 Session Cookie 同樣是為了覆蓋
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            return RedirectToAction("Index", "Login");
        }

    }
}