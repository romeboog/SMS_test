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
    public class BD01Service
    {
        private IRepository<bd01_org> db;
        public BD01Service()
        {
            db = new GenericRepository<bd01_org>();
        }        
        public IQueryable<BD01ViewModel_SM01DDL> Get_SM01DDL(int CurrPage, int PageSize, out int TotalRow)
        {
            List<BD01ViewModel_SM01DDL> model = new List<BD01ViewModel_SM01DDL>();
            //var dbresult = db.Get().AsQueryable();
            TotalRow = db.Get().ToList().Count;
            var dbresult = db.Get().ToList().Skip((CurrPage-1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                BD01ViewModel_SM01DDL _model = new BD01ViewModel_SM01DDL();
                _model.org_id = items.org_id.Trim();
                _model.org_name = items.org_name.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }
        public IQueryable<BD01ViewModel_SM01DDL> Get()
        {
            List<BD01ViewModel_SM01DDL> model = new List<BD01ViewModel_SM01DDL>();
            var dbresult = db.Get().AsQueryable();
            //TotalRow = db.Get().ToList().Count;
            //var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                BD01ViewModel_SM01DDL _model = new BD01ViewModel_SM01DDL();
                _model.org_id = items.org_id.Trim();
                _model.org_name = items.org_name.Trim(); 
                model.Add(_model);
            }
            return model.AsQueryable();

        }
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
