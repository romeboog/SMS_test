using SMS.Domain.ViewModels;
using SMS.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.DAL.Interfaces;
using SMS.Domain;

namespace SMS.BLL.Services
{
    public class LoginService
    {
        private IRepository<bd03_user> db;
        public LoginService()
        {
            db = new GenericRepository<bd03_user>();
        }
        private List<bd03_user> Getbd03()
        {
            List<bd03_user> result = db.Get().ToList();
            return result;
        }
        public bd03_user CheckUser(LoginViewModel model)
        {
            bool isfound = Getbd03().Any(x => x.user_id.Trim() == model.user_id.Trim() && x.user_pwd.Trim() == model.user_password.Trim());
            if (isfound){
                bd03_user dbdata = Getbd03().Where(x => x.user_id.Trim() == model.user_id.Trim() && x.user_pwd.Trim() == model.user_password.Trim()).First();
                return dbdata;
            }
            else
                return new bd03_user();
        }
        
    }
}
