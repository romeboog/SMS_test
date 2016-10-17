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
    public class BD03Controller : ApiController
    {
        private BD03Service service;
        private SM01Service service2;
        public BD03Controller()
        {
            service = new BD03Service();
        }

        //分頁
        [HttpGet]
        public HttpResponseMessage bd03s(int CurrPage, int PageSize, string Select_Org, string Select_Dept, string Select_Id, string Select_Name)
        {
            try
            {
                //總數量
                int TotalRow = 0;
                if (Select_Id == null || Select_Id == "")
                    Select_Id = "all";
                if (Select_Name == null || Select_Name == "")
                    Select_Name = "all";
                //向BLL取得資料
                var datas = service.Get(CurrPage, PageSize, Select_Org, Select_Dept, Select_Id, Select_Name, out TotalRow);

                //回傳一個JSON Object
                var Rvl = new { Total = TotalRow, Data = datas };
                return Request.CreateResponse(HttpStatusCode.OK, Rvl);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }

        //取得特定使用者資料
        [HttpGet]
        public HttpResponseMessage Get(string User_org, string User_dept, string User_id)
        {
            try
            {

                var datas = service.getUser(User_org, User_dept, User_id);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
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
                BD03Service BD03 = new BD03Service();
                var bd03datas = BD03.Get();
                return Request.CreateResponse(HttpStatusCode.OK, bd03datas);
                        
            }         
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }

        //傳回是否在軟體保管單有該使用者資料
        public HttpResponseMessage Get(string Sm01_user_org, string Sm01_user_dept, string Sm01_user_id, string Sm01_year)
        {
            try
            {
                var datas = service.sm01_has_user_info(Sm01_user_org, Sm01_user_dept, Sm01_user_id);
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
        //新增
        [HttpPost]
        public HttpResponseMessage Post(BD03AddModel model)
        {
            try
            {
                service.addBD03(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        //修改
       [HttpPut]
        public HttpResponseMessage Put(BD03AddModel model, string Org, string Dept, string User_id)
        {
            try
            {
                service.SaveUser(model,Org,Dept,User_id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        //刪除
        public HttpResponseMessage Delete(string user_org, string user_dept, string user_id)
        {
            try
            {
                service.Delete(user_org, user_dept, user_id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
       
    }
}
