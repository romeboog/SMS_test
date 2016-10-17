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
    public class SM01Service
    {
        private IRepository<sm01_soft_keep_main> db;
        private IRepository<bd01_org> db_BD01;
        private IRepository<bd02_dept> db_BD02;
        private IRepository<bd03_user> db_BD03;
        private IRepository<bd04_common_code> db_BD04;
        public SM01Service()
        {
            db = new GenericRepository<sm01_soft_keep_main>();
            db_BD01 = new GenericRepository<bd01_org>();
            db_BD02 = new GenericRepository<bd02_dept>();
            db_BD03 = new GenericRepository<bd03_user>();
            db_BD04 = new GenericRepository<bd04_common_code>();
        }        
        //public IQueryable<SM01ViewModel> Get(int CurrPage, int PageSize, out int TotalRow)
        //{
        //    List<SM01ViewModel> model = new List<SM01ViewModel>();
        //    //var dbresult = db.Get().AsQueryable();
        //    var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
        //    TotalRow = db.Get().Count();
        //    foreach(var items in dbresult)
        //    {
        //        SM01ViewModel _model = new SM01ViewModel();
        //        _model.year = items.year;
        //        _model.org = items.org;
        //        _model.dept = items.dept;
        //        _model.soft_id = items.soft_id;
        //        _model.soft_name = items.soft_name;
        //        _model.user_id = items.user_id;
        //        _model.soft_type = items.soft_type;
        //        _model.soft_sn = items.soft_sn;
        //        _model.soft_for = items.soft_for;
        //        _model.soft_work_on = items.soft_work_on;
        //        _model.soft_max_user = items.soft_max_user;
        //        _model.soft_number = items.soft_number;
        //        _model.soft_platform = items.soft_platform;
        //        _model.soft_from = items.soft_from;
        //        _model.soft_from_unit = items.soft_from_unit;
        //        _model.soft_keeper = items.soft_keeper;
        //        _model.soft_doc = items.soft_doc;
        //        _model.install_date = items.install_date;
        //        _model.install_place = items.install_place;
        //        _model.memo = items.memo;
        //        model.Add(_model);
        //    }
        //    return model.AsQueryable();
            
        //}
        public IQueryable<SM01ListViewModel> Get(int CurrPage, int PageSize,object[] filterpara, out int TotalRow)
        {
            List<SM01ListViewModel> model = new List<SM01ListViewModel>();
            //var dbresult = db.Get().AsQueryable();
            
            TotalRow = db.Get().ToList().Count;

            var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                SM01ListViewModel _model = new SM01ListViewModel();
                _model.year = items.year.Trim();
                _model.org = items.org.Trim();
                _model.dept = items.dept.Trim();
                _model.soft_id = items.soft_id.Trim();
                _model.soft_name = string.IsNullOrEmpty(items.soft_name) ? null : items.soft_name.Trim();
                _model.user_id = string.IsNullOrEmpty(items.user_id) ? null : items.user_id.Trim();
                _model.soft_type = string.IsNullOrEmpty(items.soft_type) ? null : items.soft_type.Trim();
                _model.soft_sn = string.IsNullOrEmpty(items.soft_sn) ? null : items.soft_sn.Trim();
                _model.soft_for = string.IsNullOrEmpty(items.soft_for) ? null : items.soft_for.Trim();
                _model.soft_work_on = string.IsNullOrEmpty(items.soft_work_on) ? null : items.soft_work_on.Trim();
                _model.soft_max_user = items.soft_max_user == null ? null : items.soft_max_user;
                _model.soft_number = items.soft_number == null ? null : items.soft_number;
                _model.soft_platform = string.IsNullOrEmpty(items.soft_platform) ? null : items.soft_platform.Trim();
                _model.soft_from = string.IsNullOrEmpty(items.soft_from) ? null : items.soft_from.Trim();
                _model.soft_from_unit = string.IsNullOrEmpty(items.soft_from_unit) ? null : items.soft_from_unit.Trim();
                _model.soft_keeper = string.IsNullOrEmpty(items.soft_keeper) ? null : items.soft_keeper.Trim();
                _model.soft_doc = string.IsNullOrEmpty(items.soft_doc) ? null : items.soft_doc.Trim();
                _model.install_date = items.install_date == null ? System.DateTime.MinValue : items.install_date.ToLocalTime() ;
                _model.install_place = string.IsNullOrEmpty(items.install_place) ? null : items.install_place.Trim();
                _model.soft_cost = items.soft_cost == null ? null : items.soft_cost;
                _model.memo = string.IsNullOrEmpty(items.memo) ? null : items.memo.Trim();
                _model.org_name = db_BD01.Get().Where(x => x.org_id == _model.org).ToList()[0].org_name.Trim();
                _model.dept_name = db_BD02.Get().Where(x => x.dept_id == _model.dept && x.dept_year == _model.year && x.dept_org == _model.org).ToList()[0].dept_name.Trim();
                _model.user_name = db_BD03.Get().Where(x => x.user_id.Trim() == _model.user_id).ToList()[0].user_name.Trim();
                _model.soft_type_name = db_BD04.Get().Where(x => x.bcc_id2.ToString() == _model.soft_type && x.bcc_type == "SFTP").ToList()[0].bcc_name.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }
        public IQueryable<SM01ListViewModel> Get(out int TotalRow)
        {
            List<SM01ListViewModel> model = new List<SM01ListViewModel>();
            var dbresult = db.Get().AsQueryable();
            TotalRow = db.Get().ToList().Count;
            //var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                SM01ListViewModel _model = new SM01ListViewModel();
                _model.year = items.year.Trim();
                _model.org = items.org.Trim();
                _model.dept = items.dept.Trim();
                _model.soft_id = items.soft_id.Trim();
                _model.soft_name = string.IsNullOrEmpty(items.soft_name) ? null : items.soft_name.Trim();
                _model.user_id = string.IsNullOrEmpty(items.user_id) ? null : items.user_id.Trim();
                _model.soft_type = string.IsNullOrEmpty(items.soft_type) ? null : items.soft_type.Trim();
                _model.soft_sn = string.IsNullOrEmpty(items.soft_sn) ? null : items.soft_sn.Trim();
                _model.soft_for = string.IsNullOrEmpty(items.soft_for) ? null : items.soft_for.Trim();
                _model.soft_work_on = string.IsNullOrEmpty(items.soft_work_on) ? null : items.soft_work_on.Trim();
                _model.soft_max_user = items.soft_max_user == null ? null : items.soft_max_user;
                _model.soft_number = items.soft_number == null ? null : items.soft_number;
                _model.soft_platform = string.IsNullOrEmpty(items.soft_platform) ? null : items.soft_platform.Trim();
                _model.soft_from = string.IsNullOrEmpty(items.soft_from) ? null : items.soft_from.Trim();
                _model.soft_from_unit = string.IsNullOrEmpty(items.soft_from_unit) ? null : items.soft_from_unit.Trim();
                _model.soft_keeper = string.IsNullOrEmpty(items.soft_keeper) ? null : items.soft_keeper.Trim();
                _model.soft_doc = string.IsNullOrEmpty(items.soft_doc) ? null : items.soft_doc.Trim();
                _model.install_date = items.install_date == null ? System.DateTime.MinValue : items.install_date.ToLocalTime();
                _model.install_place = string.IsNullOrEmpty(items.install_place) ? null : items.install_place.Trim();
                _model.soft_cost = items.soft_cost == null ? null : items.soft_cost;
                _model.memo = string.IsNullOrEmpty(items.memo) ? null : items.memo.Trim();
                _model.org_name = db_BD01.Get().Where(x => x.org_id == _model.org).ToList()[0].org_name.Trim();
                _model.dept_name = db_BD02.Get().Where(x => x.dept_id == _model.dept && x.dept_year == _model.year && x.dept_org == _model.org).ToList()[0].dept_name.Trim();
                _model.user_name = db_BD03.Get().Any(x=>x.user_id.Trim() == _model.user_id.Trim()) ? db_BD03.Get().Where(x => x.user_id.Trim() == _model.user_id).ToList()[0].user_name.Trim() : null;
                _model.soft_type_name = db_BD04.Get().Where(x => x.bcc_id2.ToString() == _model.soft_type && x.bcc_type == "SFTP").ToList()[0].bcc_name.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }
        public IQueryable<SM01ViewModel> Get()
        {
            List<SM01ViewModel> model = new List<SM01ViewModel>();
            var dbresult = db.Get().AsQueryable();
            //var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                SM01ViewModel _model = new SM01ViewModel();
                _model.year = items.year.Trim();
                _model.org = items.org.Trim();
                _model.dept = items.dept.Trim();
                _model.soft_id = items.soft_id.Trim();
                _model.soft_name = string.IsNullOrEmpty(items.soft_name) ? null : items.soft_name.Trim();
                _model.user_id = string.IsNullOrEmpty(items.user_id) ? null : items.user_id.Trim();
                _model.soft_type = string.IsNullOrEmpty(items.soft_type) ? null : items.soft_type.Trim();
                _model.soft_sn = string.IsNullOrEmpty(items.soft_sn) ? null : items.soft_sn.Trim();
                _model.soft_for = string.IsNullOrEmpty(items.soft_for) ? null : items.soft_for.Trim();
                _model.soft_work_on = string.IsNullOrEmpty(items.soft_work_on) ? null : items.soft_work_on.Trim();
                _model.soft_max_user = items.soft_max_user == null ? null : items.soft_max_user;
                _model.soft_number = items.soft_number == null ? null : items.soft_number;
                _model.soft_platform = string.IsNullOrEmpty(items.soft_platform) ? null : items.soft_platform.Trim();
                _model.soft_from = string.IsNullOrEmpty(items.soft_from) ? null : items.soft_from.Trim();
                _model.soft_from_unit = string.IsNullOrEmpty(items.soft_from_unit) ? null : items.soft_from_unit.Trim();
                _model.soft_keeper = string.IsNullOrEmpty(items.soft_keeper) ? null : items.soft_keeper.Trim();
                _model.soft_doc = string.IsNullOrEmpty(items.soft_doc) ? null : items.soft_doc.Trim();
                _model.install_date = items.install_date == null ? System.DateTime.MinValue : items.install_date.ToLocalTime();
                _model.install_place = string.IsNullOrEmpty(items.install_place) ? null : items.install_place.Trim();
                _model.memo = string.IsNullOrEmpty(items.memo) ? null : items.memo.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }
        public IQueryable<SM01ViewModel> Get(string year, string org, string dept_id)
        {
            List<SM01ViewModel> model = new List<SM01ViewModel>();
            var dbresult = db.Get().AsQueryable().Where(x=> x.dept == dept_id && x.org == org && x.year == year);
            //TotalRow = db.Get().ToList().Count;
            //var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                SM01ViewModel _model = new SM01ViewModel();
                _model.year = items.year.Trim();
                _model.org = items.org.Trim();
                _model.dept = items.dept.Trim();
                _model.soft_id = items.soft_id.Trim();
                _model.soft_name = string.IsNullOrEmpty(items.soft_name) ? null : items.soft_name.Trim();
                _model.user_id = string.IsNullOrEmpty(items.user_id) ? null : items.user_id.Trim();
                _model.soft_type = string.IsNullOrEmpty(items.soft_type) ? null : items.soft_type.Trim();
                _model.soft_sn = string.IsNullOrEmpty(items.soft_sn) ? null : items.soft_sn.Trim();
                _model.soft_for = string.IsNullOrEmpty(items.soft_for) ? null : items.soft_for.Trim();
                _model.soft_work_on = string.IsNullOrEmpty(items.soft_work_on) ? null : items.soft_work_on.Trim();
                _model.soft_max_user = items.soft_max_user == null ? null : items.soft_max_user;
                _model.soft_number = items.soft_number == null ? null : items.soft_number;
                _model.soft_cost = items.soft_cost == null ? null : items.soft_cost;
                _model.soft_platform = string.IsNullOrEmpty(items.soft_platform) ? null : items.soft_platform.Trim();
                _model.soft_from = string.IsNullOrEmpty(items.soft_from) ? null : items.soft_from.Trim();
                _model.soft_from_unit = string.IsNullOrEmpty(items.soft_from_unit) ? null : items.soft_from_unit.Trim();
                _model.soft_keeper = string.IsNullOrEmpty(items.soft_keeper) ? null : items.soft_keeper.Trim();
                _model.soft_doc = string.IsNullOrEmpty(items.soft_doc) ? null : items.soft_doc.Trim();
                _model.install_date = items.install_date == null ? System.DateTime.MinValue : items.install_date.ToLocalTime();
                _model.install_place = string.IsNullOrEmpty(items.install_place) ? null : items.install_place.Trim();
                _model.memo = string.IsNullOrEmpty(items.memo) ? null : items.memo.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }
        public SM01ViewModel Get(string year,string org, string dept, string soft_id)
        {
            List<SM01ViewModel> model = new List<SM01ViewModel>();
            //var dbresult = db.Get().AsQueryable();
            var dbresult = db.Get().ToList();
            //var result = dbresult.Find(year, org, dept, soft_id);
            int i = 0;
            bool isfound = false;
            sm01_soft_keep_main result = new sm01_soft_keep_main();
            
            while ( i < dbresult.Count && !isfound)
            {
                if (dbresult[i].year.Trim() == year && dbresult[i].org.Trim() == org && dbresult[i].dept.Trim() == dept && dbresult[i].soft_id.Trim() == soft_id)
                {
                    result = dbresult[i];
                    isfound = true;
                }
                i++;
            }
            if (isfound)
            {
                SM01ViewModel _model = new SM01ViewModel();
                _model.year = result.year.Trim();
                _model.org = result.org.Trim();
                _model.dept = result.dept.Trim();
                _model.soft_id = result.soft_id.Trim();
                _model.soft_name = string.IsNullOrEmpty(result.soft_name) ? null : result.soft_name.Trim();
                _model.user_id = string.IsNullOrEmpty(result.user_id) ? null : result.user_id.Trim();
                _model.soft_type = string.IsNullOrEmpty(result.soft_type) ? null : result.soft_type.Trim();
                _model.soft_sn = string.IsNullOrEmpty(result.soft_sn) ? null : result.soft_sn.Trim();
                _model.soft_for = string.IsNullOrEmpty(result.soft_for) ? null : result.soft_for.Trim();
                _model.soft_work_on = string.IsNullOrEmpty(result.soft_work_on) ? null : result.soft_work_on.Trim();
                _model.soft_max_user = result.soft_max_user == null ? null : result.soft_max_user;
                _model.soft_number = result.soft_number == null ? null : result.soft_number;
                _model.soft_cost = result.soft_cost == null ? null : result.soft_cost;
                _model.soft_platform = string.IsNullOrEmpty(result.soft_platform) ? null : result.soft_platform.Trim();
                _model.soft_from = string.IsNullOrEmpty(result.soft_from) ? null : result.soft_from.Trim();
                _model.soft_from_unit = string.IsNullOrEmpty(result.soft_from_unit) ? null : result.soft_from_unit.Trim();
                _model.soft_keeper = string.IsNullOrEmpty(result.soft_keeper) ? null : result.soft_keeper.Trim();
                _model.soft_doc = string.IsNullOrEmpty(result.soft_doc) ? null : result.soft_doc.Trim();
                _model.install_date = result.install_date == null ? System.DateTime.MinValue : result.install_date.ToLocalTime();
                _model.install_place = string.IsNullOrEmpty(result.install_place) ? null : result.install_place.Trim();
                _model.memo = string.IsNullOrEmpty(result.memo) ? null : result.memo.Trim();
                model.Add(_model);
                return _model;
            }
            else
                return new SM01ViewModel();
        }
        private sm01_soft_keep_main SM01VMtoM(SM01ViewModel viewmodel)
        {
            sm01_soft_keep_main _model = new sm01_soft_keep_main();
            _model.year = viewmodel.year;
            _model.org = viewmodel.org;
            _model.dept = viewmodel.dept;
            _model.soft_id = viewmodel.soft_id;
            _model.soft_name = string.IsNullOrEmpty(viewmodel.soft_name) ? null : viewmodel.soft_name.Trim() ;
            _model.user_id = string.IsNullOrEmpty(viewmodel.user_id) ? null : viewmodel.user_id.Trim();
            _model.soft_type = string.IsNullOrEmpty(viewmodel.soft_type) ? null : viewmodel.soft_type.Trim();
            _model.soft_sn = string.IsNullOrEmpty(viewmodel.soft_sn) ? null : viewmodel.soft_sn.Trim();
            _model.soft_for = string.IsNullOrEmpty(viewmodel.soft_for) ? null : viewmodel.soft_for.Trim();
            _model.soft_work_on = string.IsNullOrEmpty(viewmodel.soft_work_on) ? null : viewmodel.soft_work_on.Trim();
            _model.soft_max_user = viewmodel.soft_max_user == null ? null : viewmodel.soft_max_user;
            _model.soft_number = viewmodel.soft_number == null ? null : viewmodel.soft_number;
            _model.soft_cost = viewmodel.soft_cost == null ? null : viewmodel.soft_cost;
            _model.soft_platform = string.IsNullOrEmpty(viewmodel.soft_platform) ? null : viewmodel.soft_platform.Trim();
            _model.soft_from = string.IsNullOrEmpty(viewmodel.soft_from) ? null : viewmodel.soft_from.Trim();
            _model.soft_from_unit = string.IsNullOrEmpty(viewmodel.soft_from_unit) ? null : viewmodel.soft_from_unit.Trim();
            _model.soft_keeper = string.IsNullOrEmpty(viewmodel.soft_keeper) ? null : viewmodel.soft_keeper.Trim();
            _model.soft_doc = string.IsNullOrEmpty(viewmodel.soft_doc) ? null : viewmodel.soft_doc.Trim();
            _model.install_date = viewmodel.install_date == null ? System.DateTime.MinValue : viewmodel.install_date.ToLocalTime();
            _model.install_place = string.IsNullOrEmpty(viewmodel.install_place) ? null : viewmodel.install_place.Trim();
            _model.memo = string.IsNullOrEmpty(viewmodel.memo) ? null : viewmodel.memo.Trim();
            return _model;
        }
        private sm02_soft_keep_detail SM02VMtoM(SM02ViewModel items)
        {
            sm02_soft_keep_detail _model = new sm02_soft_keep_detail();
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
            _model.update_date = items.update_date == null ? System.DateTime.MinValue : items.update_date.ToLocalTime();
            _model.decrease_reason = string.IsNullOrEmpty(items.decrease_reason) ? null : items.decrease_reason.Trim();
            _model.decrease_handle = string.IsNullOrEmpty(items.decrease_handle) ? null : items.decrease_handle.Trim();
            _model.detail_memo = string.IsNullOrEmpty(items.detail_memo) ? null : items.detail_memo.Trim();
            return _model;
        }
        private SM01ViewModel SMVMtoSM01VM(SMViewModel items)
        {
            SM01ViewModel _model = new SM01ViewModel();
            _model.year = items.year.Trim();
            _model.org = items.org.Trim();
            _model.dept = items.dept.Trim();
            _model.soft_id = items.soft_id.Trim();
            _model.soft_name = string.IsNullOrEmpty(items.soft_name) ? null : items.soft_name.Trim();
            _model.user_id = string.IsNullOrEmpty(items.user_id) ? null : items.user_id.Trim();
            _model.soft_type = string.IsNullOrEmpty(items.soft_type) ? null : items.soft_type.Trim();
            _model.soft_sn = string.IsNullOrEmpty(items.soft_sn) ? null : items.soft_sn.Trim();
            _model.soft_for = string.IsNullOrEmpty(items.soft_for) ? null : items.soft_for.Trim();
            _model.soft_work_on = string.IsNullOrEmpty(items.soft_work_on) ? null : items.soft_work_on.Trim();
            _model.soft_max_user = items.soft_max_user == null ? null : items.soft_max_user;
            _model.soft_number = items.soft_number == null ? null : items.soft_number;
            _model.soft_cost = items.soft_cost == null ? null : items.soft_cost;
            _model.soft_platform = string.IsNullOrEmpty(items.soft_platform) ? null : items.soft_platform.Trim();
            _model.soft_from = string.IsNullOrEmpty(items.soft_from) ? null : items.soft_from.Trim();

            _model.soft_from_unit = string.IsNullOrEmpty(items.soft_from_unit) ? null : items.soft_from_unit.Trim();
            _model.soft_keeper = string.IsNullOrEmpty(items.soft_keeper) ? null : items.soft_keeper.Trim();
            _model.soft_doc = string.IsNullOrEmpty(items.soft_doc) ? null : items.soft_doc.Trim();
            _model.install_date = items.install_date == null ? System.DateTime.MinValue : items.install_date.ToLocalTime();
            _model.install_place = string.IsNullOrEmpty(items.install_place) ? null : items.install_place.Trim();
            _model.memo = string.IsNullOrEmpty(items.memo) ? null : items.memo.Trim();
            return _model;
        }
        private SM02ViewModel SMVMtoSM02VM(SMViewModel items)
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
            _model.soft_cost = items.soft_cost == null ?  null : items.soft_cost;
            _model.auth_number = items.auth_number == null ? null : items.auth_number;
            _model.update_date = items.update_date == null ? System.DateTime.MinValue : items.update_date.ToLocalTime();
            _model.decrease_reason = string.IsNullOrEmpty(items.decrease_reason) ? null : items.decrease_reason.Trim();
            _model.decrease_handle = string.IsNullOrEmpty(items.decrease_handle) ? null : items.decrease_handle.Trim();
            _model.detail_memo = string.IsNullOrEmpty(items.detail_memo) ? null : items.detail_memo.Trim();
            return _model;
        }

        //private sm02_soft_keep_detail SM01VMtoSM02VM(SM01ViewModel viewmodel)
        //{
        //    sm02_soft_keep_detail _model = new sm02_soft_keep_detail();
        //    _model.year = viewmodel.year;
        //    _model.org = viewmodel.org;
        //    _model.dept = viewmodel.dept;
        //    _model.soft_id = viewmodel.soft_id;
        //    _model.keep_org = "";
        //    return _model;
        //}

        public void addSM01(SMViewModel viewmodel)
        {
            IRepository<sm02_soft_keep_detail> db2 = new GenericRepository<sm02_soft_keep_detail>();
            SM01ViewModel SM01VM = SMVMtoSM01VM(viewmodel);
            SM02ViewModel SM02VM = SMVMtoSM02VM(viewmodel);
            sm01_soft_keep_main _SM01model = SM01VMtoM(SM01VM);
            sm02_soft_keep_detail _SM02model = SM02VMtoM(SM02VM);
            DateTime curdate = DateTime.Now;
            _SM01model.make_date = curdate;
            db.Insert(_SM01model);
            db2.Insert(_SM02model);
        }
        public void saveSM01(SM01ViewModel viewmodel)
        {
            sm01_soft_keep_main _model = SM01VMtoM(viewmodel);
            IRepository<sm01_soft_keep_main> db_getmkdate = new GenericRepository<sm01_soft_keep_main>();
            _model.make_date = db_getmkdate.Get().Where(x => x.soft_id == _model.soft_id).ToList()[0].make_date;
            db.Update(_model);
        }
        public void deleteSM01(string year, string org, string dept, string soft_id)
        {
            var sm01 = db.GetByID(year, org, dept, soft_id);
            db.Delete(sm01);
        }

    }
}
