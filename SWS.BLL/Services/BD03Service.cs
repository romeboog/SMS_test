using SMS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMS.Domain.ViewModels;
using SMS.Domain;
using SMS.DAL;
using SMS.DAL.Repository;

namespace SMS.BLL.Services
{
    public class BD03Service
    {
        private IRepository<bd03_user> db;
         private IRepository<sm01_soft_keep_main> db2;
        public BD03Service()
        {
            db = new GenericRepository<bd03_user>();
             db2 = new GenericRepository<sm01_soft_keep_main>();
        }        
        
        /// <summary>分頁程式碼</summary>
        public IQueryable<BD03ViewModel_SM01DDL> Get(int CurrPage, int PageSize, string Select_Org, string Select_Dept, string Select_Id, string Select_Name, out int TotalRow)
        {
            //定義的ViewModel
            List<BD03ViewModel_SM01DDL> model = new List<BD03ViewModel_SM01DDL>();
            List<bd03_user> dbresult = new List<bd03_user>();
            var q = db.Get().ToList();


            if (Select_Org == "all" && Select_Dept == "all" && Select_Id == "all" && Select_Name=="all")
            {
                //取得所有筆數
                TotalRow = db.Get().ToList().Count;
                //使用Linq篩選分頁
                dbresult = db.Get().OrderByDescending(e => e.user_org).ThenBy(e => e.user_dept).ThenBy(e => e.user_id).Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                //使用查詢時Linq篩選資料
                TotalRow = db.Get().Where(u => u.user_org == (Select_Org.Equals("all") ? u.user_org : Select_Org) && u.user_dept == (Select_Dept.Equals("all") ? u.user_dept : Select_Dept) && u.user_id.Trim() == (Select_Id.Equals("all") ? u.user_id.Trim() : Select_Id) && u.user_name.Trim() == (Select_Name.Equals("all") ? u.user_name.Trim() : Select_Name)).ToList().Count;
                dbresult = db.Get().Where(u => u.user_org == (Select_Org.Equals("all") ? u.user_org : Select_Org) && u.user_dept == (Select_Dept.Equals("all") ? u.user_dept : Select_Dept) && u.user_id.Trim() == (Select_Id.Equals("all") ? u.user_id.Trim() : Select_Id) && u.user_name.Trim() == (Select_Name.Equals("all") ? u.user_name.Trim() : Select_Name)).OrderByDescending(e => e.user_org).ThenBy(e => e.user_dept).ThenBy(e => e.user_id).Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            }

            //從資料讀出後傳回列表
            foreach (var items in dbresult)
            {
               
                BD03ViewModel_SM01DDL _model = new BD03ViewModel_SM01DDL();
                _model.usable = items.usable;
                _model.user_id = items.user_id.Trim();
                _model.user_name = items.user_name.Trim();
                _model.user_org = items.user_org.Trim();
                _model.user_dept = items.user_dept.Trim();
                _model.user_tel = items.user_tel.Trim();
                _model.user_mail = items.user_mail.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }

        /// <summary>取回所有使用者資料</summary>
        public IQueryable<BD03ViewModel_SM01DDL> Get()
        {
            List<BD03ViewModel_SM01DDL> model = new List<BD03ViewModel_SM01DDL>();
            var dbresult = db.Get().AsQueryable();
            foreach (var items in dbresult)
            {
                BD03ViewModel_SM01DDL _model = new BD03ViewModel_SM01DDL();
                _model.usable = items.usable;
                _model.user_id = items.user_id.Trim();
                _model.user_name = items.user_name.Trim();
                _model.user_org = items.user_org.Trim();
                _model.user_dept = items.user_dept.Trim();
                _model.user_tel = items.user_tel.Trim();
                _model.user_mail = items.user_mail.Trim();
                model.Add(_model);
            }
            model.OrderBy(e => e.user_org).ThenBy(e => e.user_dept).ThenBy(e => e.user_id);
            return model.AsQueryable();
        }

        /// <summary>取得特定使用者資料</summary>
        public BD03AddModel getUser(string user_org, string user_dept, string user_id)
        {
            List<bd03_user> model = new List<bd03_user>();
            //var dbresult = db.Get().ToList();
           var dbresult= db.GetByUserInfo(user_org, user_dept, user_id);
            //int i = 0;
            bool isfound = false;
            bd03_user result = new bd03_user();
            //whil;le (i < dbresult.Count)
            //{
                if (dbresult.user_org.Trim() == user_org && dbresult.user_dept.Trim() == user_dept && dbresult.user_id.Trim() == user_id)
                {
                    result = dbresult;
                    isfound = true;
                }
            //    i++;
            //}
            if (isfound)
            {

                BD03AddModel _model = new BD03AddModel();
                _model.user_org = result.user_org.Trim();
                _model.user_dept = result.user_dept.Trim();
                _model.user_id = result.user_id.Trim();
                _model.user_name = result.user_name.Trim();
                _model.user_pwd = result.user_pwd.Trim();
                _model.user_sex = result.user_sex.Trim();
                _model.user_mail = result.user_mail.Trim();
                _model.user_tel = result.user_tel.Trim();
                _model.auth_type = result.auth_type;
                _model.usable = result.usable;
                return (_model);
            }
            else
                return new BD03AddModel();
          
        }

        /// <summary>修改使用者資料</summary>
        public void SaveUser(BD03AddModel viewmodel, string user_org, string user_dept, string user_id)
        {
            var dbresult = db.GetByUserInfo(user_org, user_dept, user_id);
            Delete(user_org, user_dept, user_id);
            dbresult.user_org = viewmodel.user_org;
            dbresult.user_dept = viewmodel.user_dept;
            dbresult.user_id = viewmodel.user_id;
            dbresult.user_name = viewmodel.user_name;
            dbresult.user_pwd = viewmodel.user_pwd;
            dbresult.user_sex = viewmodel.user_sex;
            dbresult.user_mail = viewmodel.user_mail;
            dbresult.user_tel = viewmodel.user_tel;
            dbresult.auth_type = viewmodel.auth_type;
            dbresult.usable = viewmodel.usable;
            dbresult.modify_date = DateTime.Now;
            db.Insert(dbresult);
            //db.Update(dbresult);

        }

        /// <summary>新增BD03使用者資料</summary>
        public void addBD03(BD03AddModel viewmodel)
        {
            bd03_user _model = new bd03_user();
            _model.user_org = viewmodel.user_org;
            _model.user_dept = viewmodel.user_dept;
            _model.user_id = viewmodel.user_id;
            _model.user_name = viewmodel.user_name;
            _model.user_pwd = viewmodel.user_pwd;
            _model.user_sex = viewmodel.user_sex;
            _model.user_mail = viewmodel.user_mail;
            _model.user_tel = viewmodel.user_tel;
            _model.auth_type = viewmodel.auth_type;
            _model.usable = viewmodel.usable;
            _model.make_date = DateTime.Now;
            _model.modify_date = DateTime.Now;
            _model.maker_id = "evan";
            db.Insert(_model);
          
        }

          /// <summary>刪除使用者資料</summary>
        /// <param name="user_org"、"user_dept"、"user_id"></param>
        public void Delete(string user_org,string user_dept,string user_id)
        {
            var User = db.GetByUserInfo(user_org, user_dept,user_id);
            db.Delete(User);
        }

  /// <summary>判斷是否在軟體保管單有該使用者資料</summary>
        /// <param name="user_org"、"user_dept"、"user_id"></param>
          public bool sm01_has_user_info( string user_org, string user_dept, string user_id)
        {
            var db2result = db2.Get().Where(u => u.org.Trim() == user_org && u.dept == user_dept.Trim() && u.user_id.Trim() == user_id).Count();
            if (db2result != 0)
                return true;
            else
                return false;

        }
    }
}
