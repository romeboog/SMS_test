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
    public class SM01Controller : ApiController
    {
        private SM01Service service;
        private SM02Service service02;
        public SM01Controller()
        {
            service = new SM01Service();
            service02 = new SM02Service();
        }

        [HttpGet]
        //public HttpResponseMessage sm01s(int CurrPage, int PageSize)
        //{
        //    try 
        //    {
        //        int TotalRow = 0;
        //        var datas = service.Get(CurrPage,PageSize,out TotalRow);
        //        var Rvl = new { Total = TotalRow, Data = datas };
        //        //var datas = service.Get();
        //        return Request.CreateResponse(HttpStatusCode.OK, Rvl);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }
        
        //}
        public HttpResponseMessage sm01s()
        {
            try
            {
                int TotalRow = 0;
                var datas = service.Get(out TotalRow);
                var Rvl = new { Total = TotalRow, Data = datas };
                //var datas = service.Get();
                return Request.CreateResponse(HttpStatusCode.OK,  Rvl);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
        [HttpGet]
        public HttpResponseMessage Get(string org,string year, string dept, string soft_id)
        {
            try
            {
                var datas = service.Get(year,org,dept,soft_id);
                //var Rvl = new { Total = TotalRow, Data = datas };
                //var datas = service.Get();
                return Request.CreateResponse(HttpStatusCode.OK, datas);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
        public HttpResponseMessage Get(string org, string year, string dept_id)
        {
            try
            {
                var datas = service.Get(year, org, dept_id);
                if (datas.Any())
                {
                    int num = int.Parse(datas.OrderByDescending(x => x.soft_id).FirstOrDefault().soft_id.Substring(11,4));
                    var Rvl = new { CurrentMaxID = num, Data = datas };
                    return Request.CreateResponse(HttpStatusCode.OK, Rvl);
                }
                else
                {
                    var Rvl = new { CurrentMaxID = 0, Data = datas };
                    return Request.CreateResponse(HttpStatusCode.OK, Rvl);
                }                
                //var datas = service.Get();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
        //public HttpResponseMessage Get(int type)
        //{
        //    try
        //    {
        //        switch(type)
        //        {
        //            case 1 :
        //                BD01Service BD01 = new BD01Service();
        //                var bd01datas = BD01.Get();
        //                return Request.CreateResponse(HttpStatusCode.OK, bd01datas);
                        
        //            case 2 :
        //                BD02Service BD02 = new BD02Service();
        //                var bd02datas = BD02.Get();
        //                return Request.CreateResponse(HttpStatusCode.OK, bd02datas);
                        
        //            case 3 :
        //                BD03Service BD03 = new BD03Service();
        //                var bd03datas = BD03.Get();
        //                return Request.CreateResponse(HttpStatusCode.OK, bd03datas);
                        
        //        }
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, "Unable Get Datas");
        //        //int TotalRow = 0;
        //        //var datas = BD01S.Get();
        //        //var Rvl = new { Total = TotalRow, Data = datas };
        //        //var datas = service.Get();
        //        //return Request.CreateResponse(HttpStatusCode.OK, datas);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }

        //}
        //public HttpResponseMessage Get(int type, string year, string org)
        //{
        //    try
        //    {
        //        switch (type)
        //        {
        //            case 2:
        //                BD02Service BD02 = new BD02Service();
        //                var bd02datas = BD02.Get();
        //                bd02datas = bd02datas.Where(bd02 => bd02.dept_year == year && bd02.dept_org == org);
        //                return Request.CreateResponse(HttpStatusCode.OK, bd02datas);
        //            default:
        //                return Request.CreateResponse(HttpStatusCode.BadRequest, "Unable Get Datas");
        //        }
        //        //int TotalRow = 0;
        //        //var datas = BD01S.Get();
        //        //var Rvl = new { Total = TotalRow, Data = datas };
        //        //var datas = service.Get();
        //        //return Request.CreateResponse(HttpStatusCode.OK, datas);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }

        //}
        public HttpResponseMessage Post(SMViewModel model)
        {
            try
            {
                service.addSM01(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }     
        public HttpResponseMessage Put(SM01ViewModel model)
        {
            try
            {
                service.saveSM01(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
        public HttpResponseMessage Delete(string year, string org, string dept, string soft_id)
        {
            try
            {
                service.deleteSM01(year,org,dept,soft_id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }

        //// GET api/sm01
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/sm01/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/sm01
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/sm01/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/sm01/5
        //public void Delete(int id)
        //{
        //}
    }
}
