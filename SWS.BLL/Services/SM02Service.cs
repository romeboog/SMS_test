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
    public class SM02Service
    {
        private IRepository<sm02_soft_keep_detail> db;
        public SM02Service()
        {
            db = new GenericRepository<sm02_soft_keep_detail>();
        }        
        //public IQueryable<SM02ViewModel> Get()
        //{
        //    List<SM02ViewModel> model = new List<SM02ViewModel>();
        //    var dbresult = db.Get().AsQueryable();
        //    //TotalRow = db.Get().ToList().Count;
        //    //var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
        //    foreach (var items in dbresult)
        //    {
        //        SM02ViewModel _model = new SM02ViewModel();
        //        _model.year = items.year.Trim();
        //        _model.org= items.org.Trim();
        //        _model.dept= items.dept.Trim();
        //        _model.soft_id= items.soft_id.Trim();
        //        _model.detail_id= items.detail_id.Trim();
        //        _model.keep_org= items.keep_org.Trim();
        //        _model.keep_man= items.keep_man.Trim();
        //        _model.use_org= items.use_org.Trim();
        //        _model.use_man= items.use_man.Trim();
        //        _model.soft_ver= items.use_man.Trim();
        //        _model.soft_cost= items.soft_cost;
        //        _model.auth_number= items.auth_number;
        //        _model.update_date= items.update_date;
        //        _model.decrease_reason= items.decrease_reason.Trim();
        //        _model.decrease_handle= items.decrease_handle.Trim();
        //        _model.detail_memo = items.detail_memo.Trim();
        //        model.Add(_model);
        //    }
        //    return model.AsQueryable();

        //}
        public SM02ViewModel Get(string year, string org, string dept, string soft_id,int detail_id )
        {
            List<SM02ViewModel> model = new List<SM02ViewModel>();
            //var dbresult = db.Get().AsQueryable();
            var dbresult = db.Get().ToList();
            //var result = dbresult.Find(year, org, dept, soft_id);
            int i = 0;
            bool isfound = false;
            sm02_soft_keep_detail result = new sm02_soft_keep_detail();

            while (i < dbresult.Count)
            {
                if (dbresult[i].year.Trim() == year && dbresult[i].org.Trim() == org && dbresult[i].dept.Trim() == dept && dbresult[i].soft_id.Trim() == soft_id && dbresult[i].detail_id == detail_id)
                {
                    result = dbresult[i];
                    isfound = true;
                }
                i++;
            }
            if (isfound)
            {
                SM02ViewModel _model = new SM02ViewModel();
                _model.year = result.year.Trim();
                _model.org = result.org.Trim();
                _model.dept = result.dept.Trim();
                _model.soft_id = result.soft_id.Trim();
                _model.detail_id = result.detail_id;
                _model.keep_org = string.IsNullOrEmpty(result.keep_org) ? null : result.keep_org.Trim();
                _model.keep_man = string.IsNullOrEmpty(result.keep_man) ? null : result.keep_man.Trim();
                _model.use_org = string.IsNullOrEmpty(result.use_org) ? null : result.use_org.Trim();
                _model.use_man = string.IsNullOrEmpty(result.use_man) ? null : result.use_man.Trim();
                _model.soft_ver = string.IsNullOrEmpty(result.soft_ver) ? null : result.soft_ver.Trim();
                _model.soft_cost = result.soft_cost == null ? null : result.soft_cost;
                _model.auth_number = result.auth_number == null ? null : result.auth_number;
                _model.update_date = result.update_date == null ? System.DateTime.MinValue : result.update_date.ToLocalTime();
                _model.decrease_reason = string.IsNullOrEmpty(result.decrease_reason) ? result.decrease_reason.Trim() : null;
                _model.decrease_handle = string.IsNullOrEmpty(result.decrease_handle) ? result.decrease_handle.Trim() : null;
                _model.detail_memo = string.IsNullOrEmpty(result.detail_memo) ? null : result.detail_memo.Trim();
                model.Add(_model);
                return _model;
            }
            else
                return new SM02ViewModel();
        }
        public IQueryable<SM02ViewModel> Get(string year, string org, string dept, string soft_id, int CurrPage, int PageSize, out int TotalRow)
        {
            List<SM02ViewModel> model = new List<SM02ViewModel>();
            //var dbresult = db.Get().AsQueryable();
            //TotalRow = db.Get().ToList().Count;
            var dbresult_all = db.Get().Where(r => r.org == org && r.dept == dept && r.soft_id == soft_id && r.year == year).AsQueryable();
            //var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).AsQueryable();
            
            var dbresult = dbresult_all.Where(r => r.org == org && r.dept == dept && r.soft_id == soft_id && r.year == year);
            TotalRow = dbresult_all.Count();
            dbresult = dbresult.Skip(TotalRow - (CurrPage * PageSize)).Take(PageSize).AsQueryable();
            
            foreach (var items in dbresult)
            {
                SM02ViewModel _model = new SM02ViewModel();
                _model.year = items.year.Trim();
                _model.org = items.org.Trim();
                _model.dept = items.dept.Trim();
                _model.soft_id = items.soft_id.Trim();
                _model.detail_id = items.detail_id;
                _model.keep_org = string.IsNullOrEmpty(items.keep_org) ? null : items.keep_org.Trim() ;
                _model.keep_man = string.IsNullOrEmpty(items.keep_man) ? null : items.keep_man.Trim();
                _model.use_org = string.IsNullOrEmpty(items.use_org) ? null : items.use_org.Trim();
                _model.use_man = string.IsNullOrEmpty(items.use_man) ? null : items.use_man.Trim();
                _model.soft_ver = string.IsNullOrEmpty(items.soft_ver) ? null : items.soft_ver.Trim();
                _model.soft_cost = items.soft_cost == null ? null : items.soft_cost;
                _model.auth_number = items.auth_number == null ? null : items.auth_number ;
                _model.update_date = items.update_date == null ? DateTime.MinValue : items.update_date.ToLocalTime();
                _model.decrease_reason = string.IsNullOrEmpty(items.decrease_reason) ? null : items.decrease_reason.Trim();
                _model.decrease_handle = string.IsNullOrEmpty(items.decrease_handle) ? null : items.decrease_handle.Trim();
                _model.detail_memo = string.IsNullOrEmpty(items.detail_memo) ? null : items.detail_memo.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }
        public IQueryable<SM02ViewModel> Get(string year, string org, string dept, string soft_id)
        {
            List<SM02ViewModel> model = new List<SM02ViewModel>();
            var dbresult = db.Get().AsQueryable();
            //TotalRow = db.Get().ToList().Count;
            //var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).AsQueryable();
            dbresult = dbresult.Where(r => r.org == org && r.dept == dept && r.soft_id == soft_id && r.year == year);
            //TotalRow = dbresult.Count();
            foreach (var items in dbresult)
            {
                SM02ViewModel _model = new SM02ViewModel();
                _model.year = items.year.Trim();
                _model.org = items.org.Trim();
                _model.dept = items.dept.Trim();
                _model.soft_id = items.soft_id.Trim();
                _model.detail_id = items.detail_id;
                _model.keep_org = string.IsNullOrEmpty(items.keep_org) ? null : items.keep_org.Trim();
                _model.keep_man = string.IsNullOrEmpty(items.keep_man) ? null : items.keep_man.Trim();
                _model.use_org = string.IsNullOrEmpty(items.use_org) ? null : items.use_org.Trim();
                _model.use_man = string.IsNullOrEmpty(items.use_man) ? null : items.use_man.Trim();
                _model.soft_ver = string.IsNullOrEmpty(items.soft_ver) ? null : items.soft_ver.Trim();
                _model.soft_cost = items.soft_cost == null ? null : items.soft_cost;
                _model.auth_number = items.auth_number == null ? null : items.auth_number;
                _model.update_date = items.update_date == null ? DateTime.MinValue : items.update_date.ToLocalTime();
                _model.decrease_reason = string.IsNullOrEmpty(items.decrease_reason) ? null : items.decrease_reason.Trim();
                _model.decrease_handle = string.IsNullOrEmpty(items.decrease_handle) ? null : items.decrease_handle.Trim();
                _model.detail_memo = string.IsNullOrEmpty(items.detail_memo) ? null : items.detail_memo.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }
        private sm02_soft_keep_detail SM02VMtoM(SM02ViewModel viewmodel)
        {
            sm02_soft_keep_detail _model = new sm02_soft_keep_detail();
            _model.year = viewmodel.year.Trim();
            _model.org = viewmodel.org.Trim();
            _model.dept = viewmodel.dept.Trim();
            _model.soft_id = viewmodel.soft_id.Trim();
            _model.detail_id = viewmodel.detail_id;
            _model.keep_org = string.IsNullOrEmpty(viewmodel.keep_org) ? null : viewmodel.keep_org.Trim();
            _model.keep_man = string.IsNullOrEmpty(viewmodel.keep_man) ? null : viewmodel.keep_man.Trim();
            _model.use_org = string.IsNullOrEmpty(viewmodel.use_org) ? null : viewmodel.use_org.Trim();
            _model.use_man = string.IsNullOrEmpty(viewmodel.use_man) ? null : viewmodel.use_man.Trim();
            _model.soft_ver = string.IsNullOrEmpty(viewmodel.soft_ver) ? null : viewmodel.soft_ver.Trim();
            _model.soft_cost = viewmodel.soft_cost == null ? null : viewmodel.soft_cost;
            _model.auth_number = viewmodel.auth_number == null ? null : viewmodel.auth_number;
            _model.update_date = viewmodel.update_date == null ? System.DateTime.MinValue : viewmodel.update_date.ToLocalTime();
            _model.decrease_reason = string.IsNullOrEmpty(viewmodel.decrease_reason) ? null : viewmodel.decrease_reason.Trim();
            _model.decrease_handle = string.IsNullOrEmpty(viewmodel.decrease_handle) ? null : viewmodel.decrease_handle.Trim();
            _model.detail_memo = string.IsNullOrEmpty(viewmodel.detail_memo) ? null : viewmodel.detail_memo.Trim();
            return _model;
        }

        public void addSM02(SM02ViewModel viewmodel)
        {
            sm02_soft_keep_detail _model = SM02VMtoM(viewmodel);
            db.Insert(_model);
        }
        public void saveSM02(SM02ViewModel viewmodel)
        {
            sm02_soft_keep_detail _model = SM02VMtoM(viewmodel);
            db.Update(_model);
        }
        public void deleteSM02(string year, string org, string dept, string soft_id, int detail_id)
        {
            var sm02 = db.GetByID(year, org, dept, soft_id,detail_id);
            db.Delete(sm02);
        }
        //public List<SM01_GetUsableOrg_Result> {}
    }
}
