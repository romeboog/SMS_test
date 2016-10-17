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
    public class BD04Service
    {
        private IRepository<bd04_common_code> db;
        public BD04Service()
        {
            db = new GenericRepository<bd04_common_code>();
        }
        public IQueryable<BD04ViewModel> Get()
        {
            List<BD04ViewModel> model = new List<BD04ViewModel>();
            var dbresult = db.Get().OrderBy(x=>x.bcc_type).ThenBy(x=>x.bcc_id2).AsQueryable();
            //TotalRow = db.Get().ToList().Count;
            //var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                BD04ViewModel _model = new BD04ViewModel();
                _model.bcc_id2 = items.bcc_id2;
                _model.bcc_type = items.bcc_type.Trim();
                _model.bcc_name = items.bcc_name.Trim(); 
                model.Add(_model);
            }
            return model.AsQueryable();

        }
        public IQueryable<BD04ViewModel> Get(string bcc_type)
        {
            List<BD04ViewModel> model = new List<BD04ViewModel>();
            var dbresult = db.Get().Where(x=>x.bcc_type.Trim() == bcc_type).OrderBy(x=>x.bcc_id2).AsQueryable();
            //TotalRow = db.Get().ToList().Count;
            //var dbresult = db.Get().ToList().Skip((CurrPage - 1) * PageSize).Take(PageSize).ToList();
            foreach (var items in dbresult)
            {
                BD04ViewModel _model = new BD04ViewModel();
                _model.bcc_id2 = items.bcc_id2;
                _model.bcc_type = items.bcc_type.Trim();
                _model.bcc_name = items.bcc_name.Trim();
                model.Add(_model);
            }
            return model.AsQueryable();

        }
    }
}
