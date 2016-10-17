using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMS.BLL.Services;
using SMS.Domain.ViewModels;


namespace SMS.Web.API
{
    public class BD02Controller : ApiController
    {
        private BD02Service service;
        public BD02Controller()
        {
            service = new BD02Service();
        }

        //分頁
        [HttpGet]
        public HttpResponseMessage bd02s(int CurrPage, int PageSize)
        {
            try
            {
                //總數量
                int TotalRow = 0;

                //向BLL取得資料
                var datas = service.Get(CurrPage, PageSize, out TotalRow);
                //回傳一個JSON Object
                var Rvl = new { Total = TotalRow, Data = datas };
                return Request.CreateResponse(HttpStatusCode.OK, Rvl);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }

        //取得所有使用者資料
        public HttpResponseMessage Get()
        {
            try
            {
                BD02Service BD02 = new BD02Service();
                var bd02datas = BD02.Get_Dept();
                return Request.CreateResponse(HttpStatusCode.OK, bd02datas);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
        public HttpResponseMessage Get(string year, string org)
        {
            try
            {
                BD02Service BD02 = new BD02Service();
                var bd02datas = BD02.Get();
                bd02datas = bd02datas.Where(bd02 => bd02.dept_year == year && bd02.dept_org == org);
                return Request.CreateResponse(HttpStatusCode.OK, bd02datas);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
        ////取得特定使用者資料
        //public HttpResponseMessage Get(string dept_year, string dept_org, string dept_id)
        //{
        //    try
        //    {
        //        var datas = service.getDept(dept_year, dept_org, dept_id);
        //        return Request.CreateResponse(HttpStatusCode.OK, datas);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }

        //}
        ////新增
        //[HttpPost]
        //public HttpResponseMessage Post(BD02AddModel model)
        //{
        //    try
        //    {
        //        service.addBD02(model);
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }
        //}

        //修改
        [HttpPut]
        public HttpResponseMessage DeptUpdate(BD02AddModel model, string year, string org, string dept)
        {
            try
            {
                service.SaveDept(model, year, org, dept);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        ////刪除
        //public HttpResponseMessage Delete(string user_org, string user_dept, string user_id)
        //{
        //    try
        //    {
        //        service.Delete(user_org, user_dept, user_id);
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }
        //}

    }
}
