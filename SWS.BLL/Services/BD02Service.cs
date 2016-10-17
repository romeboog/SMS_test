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
    public class BD02Service
    {
        private IRepository<bd02_dept> db;
        private IRepository<bd01_org> db_BD01;
        public BD02Service()
        {
            db = new GenericRepository<bd02_dept>();
            db_BD01 = new GenericRepository<bd01_org>();
        }
        public IQueryable<BD02ViewModel_SM01DDL> Get_SM01DDL(int CurrPage, int PageSize, out int TotalRow)
        {
            List<BD02ViewModel_SM01DDL> model = new List<BD02ViewModel_SM01DDL>();
            //var dbresult = db.Get().AsQueryable();
            TotalRow = db.Get().ToList().Count;
            var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                BD02ViewModel_SM01DDL _model = new BD02ViewModel_SM01DDL();
                _model.dept_id = items.dept_id.Trim();
                _model.dept_name = items.dept_name.Trim();
                _model.dept_org = items.dept_org.Trim();
                _model.dept_year = items.dept_year.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }
        public IQueryable<BD02ViewModel_SM01DDL> Get(int CurrPage, int PageSize, out int TotalRow)//RabyHungAdd_2016-10-05
        {
            List<BD02ViewModel_SM01DDL> model = new List<BD02ViewModel_SM01DDL>();
            //var dbresult = db.Get().AsQueryable();
            TotalRow = db.Get().ToList().Count;
            var dbresult = db.Get().ToList().OrderBy(e => e.dept_year).ThenBy(e => e.dept_org).ThenBy(e => e.dept_id).Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                BD02ViewModel_SM01DDL _model = new BD02ViewModel_SM01DDL();
                _model.dept_year = items.dept_year.Trim();
                _model.dept_org = items.dept_org.Trim();
                _model.org_name = db_BD01.Get().Where(x => x.org_id == _model.dept_org).ToList()[0].org_name.Trim(); //取得bd01_org資料表機關名稱，條件為：該表org_id等於model_view表的dept_org id
                _model.dept_id = items.dept_id.Trim();
                _model.dept_name = items.dept_name.Trim();
                _model.dept_memo = items.dept_memo.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }
        public IQueryable<BD02ViewModel_SM01DDL> Get()
        {
            List<BD02ViewModel_SM01DDL> model = new List<BD02ViewModel_SM01DDL>();
            var dbresult = db.Get().AsQueryable();
            //TotalRow = db.Get().ToList().Count;
            //var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                BD02ViewModel_SM01DDL _model = new BD02ViewModel_SM01DDL();
                _model.dept_id = items.dept_id.Trim();
                _model.dept_name = items.dept_name.Trim();
                _model.dept_org = items.dept_org.Trim();
                _model.dept_year = items.dept_year.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }
        public IQueryable<BD02ViewModel_SM01DDL> Get_Dept()//RabyHungAdd_2016-10-05
        {
            List<BD02ViewModel_SM01DDL> model = new List<BD02ViewModel_SM01DDL>();
            var dbresult = db.Get().AsQueryable();
            //TotalRow = db.Get().ToList().Count;
            //var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                BD02ViewModel_SM01DDL _model = new BD02ViewModel_SM01DDL();
                _model.dept_year = items.dept_year.Trim();
                _model.dept_org = items.dept_org.Trim();
                _model.org_name = db_BD01.Get().Where(x => x.org_id == _model.dept_org).ToList()[0].org_name.Trim(); //取得bd01_org資料表機關名稱，條件為：該表org_id等於model_view表的dept_org id
                _model.dept_id = items.dept_id.Trim();
                _model.dept_name = items.dept_name.Trim();
                _model.dept_memo = items.dept_memo.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }

        public void SaveDept(BD02AddModel viewmodel, string year, string org, string dept)//RabyHungAdd_2016-10-05
        {
            var dbresult = db.GetByDeptInfo(year, org, dept);
            //Delete(user_org, user_dept, user_id);
            dbresult.dept_year = viewmodel.dept_year;
            dbresult.dept_org = viewmodel.dept_org;
            dbresult.dept_id = viewmodel.dept_id;
            dbresult.dept_name = viewmodel.dept_name;
            dbresult.dept_memo = viewmodel.dept_memo;
            dbresult.make_date = DateTime.Now;
            db.Insert(dbresult);
            //db.Update(dbresult);

        }
        //public BD02AddModel getDept(string dept_year, string dept_org, string dept_id)
        //{
        //    List<bd02_dept> model = new List<bd02_dept>();
        //    //var dbresult = db.Get().ToList();
        //    var dbresult = db.GetByDeptInfo(dept_year, dept_org, dept_id);
        //    int i = 0;
        //    bool isfound = false;
        //    bd02_dept result = new bd02_dept();
        //    //whil;le (i < dbresult.Count)
        //    //{
        //    if (dbresult.dept_year.Trim() == dept_year && dbresult.dept_org.Trim() == dept_org && dbresult.dept_id.Trim() == dept_id)
        //    {
        //        result = dbresult;
        //        isfound = true;
        //    }
        //    //    i++;
        //    //}
        //    if (isfound)
        //    {

        //        BD02AddModel _model = new BD02AddModel();
        //        _model.dept_year = result.dept_year.Trim();
        //        _model.dept_org = result.dept_org.Trim();
        //        _model.dept_id = result.dept_id.Trim();
        //        _model.dept_name = result.dept_name.Trim();
        //        _model.dept_memo = result.dept_memo.Trim();
        //        //_model.make_date = result.make_date;

        //        return (_model);
        //    }
        //    else
        //        return new BD02AddModel();

        //}
        //public SM01ViewModel Get(string year,string org, string dept, string soft_id)
        //{
        //    List<SM01ViewModel> model = new List<SM01ViewModel>();
        //    //var dbresult = db.Get().AsQueryable();
        //    var dbresult = db.Get().ToList();
        //    //var result = dbresult.Find(year, org, dept, soft_id);
        //    int i = 0;
        //    bool isfound = false;
        //    sm01_soft_keep_main result = new sm01_soft_keep_main();

        //    while ( i < dbresult.Count)
        //    {
        //        if (dbresult[i].year.Trim() == year && dbresult[i].org.Trim() == org && dbresult[i].dept.Trim() == dept && dbresult[i].soft_id.Trim() == soft_id)
        //        {
        //            result = dbresult[i];
        //            isfound = true;
        //        }
        //        i++;
        //    }
        //    if (isfound)
        //    {
        //        SM01ViewModel _model = new SM01ViewModel();
        //        _model.year = result.year.Trim();
        //        _model.org = result.org.Trim();
        //        _model.dept = result.dept.Trim();
        //        _model.soft_id = result.soft_id.Trim();
        //        _model.soft_name = result.soft_name.Trim();
        //        _model.user_id = result.user_id.Trim();
        //        _model.soft_type = result.soft_type.Trim();
        //        _model.soft_sn = result.soft_sn.Trim();
        //        _model.soft_for = result.soft_for.Trim();
        //        _model.soft_work_on = result.soft_work_on.Trim();
        //        _model.soft_max_user = result.soft_max_user;
        //        _model.soft_number = result.soft_number;
        //        _model.soft_platform = result.soft_platform.Trim();
        //        _model.soft_from = result.soft_from.Trim();
        //        _model.soft_from_unit = result.soft_from_unit.Trim();
        //        _model.soft_keeper = result.soft_keeper.Trim();
        //        _model.soft_doc = result.soft_doc.Trim();
        //        _model.install_date = result.install_date;
        //        _model.install_place = result.install_place.Trim();
        //        _model.memo = result.memo.Trim();
        //        model.Add(_model);
        //        return _model;
        //    }
        //    else
        //        return new SM01ViewModel();
        //}

        //private sm01_soft_keep_main SM01VMtoM(SM01ViewModel viewmodel)
        //{
        //    sm01_soft_keep_main _model = new sm01_soft_keep_main();
        //    _model.year = viewmodel.year;
        //    _model.org = viewmodel.org;
        //    _model.dept = viewmodel.dept;
        //    _model.soft_id = viewmodel.soft_id;
        //    _model.soft_name = viewmodel.soft_name;
        //    _model.user_id = viewmodel.user_id;
        //    _model.soft_type = viewmodel.soft_type;
        //    _model.soft_sn = viewmodel.soft_sn;
        //    _model.soft_for = viewmodel.soft_for;
        //    _model.soft_work_on = viewmodel.soft_work_on;
        //    _model.soft_max_user = viewmodel.soft_max_user;
        //    _model.soft_number = viewmodel.soft_number;
        //    _model.soft_platform = viewmodel.soft_platform;
        //    _model.soft_from = viewmodel.soft_from;
        //    _model.soft_from_unit = viewmodel.soft_from_unit;
        //    _model.soft_keeper = viewmodel.soft_keeper;
        //    _model.soft_doc = viewmodel.soft_doc;
        //    _model.install_date = viewmodel.install_date;
        //    _model.install_place = viewmodel.install_place;
        //    _model.memo = viewmodel.memo;
        //    return _model;
        //}

        //public void addSM01(SM01ViewModel viewmodel)
        //{
        //    sm01_soft_keep_main _model = SM01VMtoM(viewmodel);
        //    db.Insert(_model);
        //}
        //public void saveSM01(SM01ViewModel viewmodel)
        //{
        //    sm01_soft_keep_main _model = SM01VMtoM(viewmodel);
        //    db.Update(_model);
        //}
        //public void deleteSM01(string year, string org, string dept, string soft_id)
        //{
        //    var sm01 = db.GetByID(year, org, dept, soft_id);
        //    db.Delete(sm01);
        //}
        //public List<SM01_GetUsableOrg_Result> {}
    }
}
