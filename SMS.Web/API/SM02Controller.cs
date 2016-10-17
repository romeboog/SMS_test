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
    public class SM02Controller : ApiController
    {
        private SM02Service service;
        public SM02Controller()
        {
            service = new SM02Service();
        }

        //[HttpGet]
        //public HttpResponseMessage sm02s(int CurrPage, int PageSize)
        //{
        //    try
        //    {
        //        int TotalRow = 0;
        //        var datas = service.Get(CurrPage, PageSize, out TotalRow);
        //        var Rvl = new { Total = TotalRow, Data = datas };
        //        //var datas = service.Get();
        //        return Request.CreateResponse(HttpStatusCode.OK, Rvl);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }

        //}
        [HttpGet]
        public HttpResponseMessage Get(string org, string year, string dept, string soft_id, int CurrPage, int PageSize)
        {
            try
            {
                int totalrow;
                var datas = service.Get(year,org,dept,soft_id,CurrPage,PageSize,out totalrow).OrderByDescending(x=>x.detail_id);
                int num = datas.OrderByDescending(x => x.detail_id).FirstOrDefault().detail_id;
                //int num = datas
                var Rvl = new { Total = totalrow, Data = datas };
                return Request.CreateResponse(HttpStatusCode.OK, Rvl);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
        public HttpResponseMessage Get(string org, string year, string dept, string soft_id)
        {
            try
            {
                int totalrow;
                var datas = service.Get(year, org, dept, soft_id).OrderByDescending(x => x.detail_id);
                int num = datas.OrderByDescending(x => x.detail_id).FirstOrDefault().detail_id;
                var Rvl = new { Total = num, Data = datas };
                return Request.CreateResponse(HttpStatusCode.OK, Rvl);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
        public HttpResponseMessage Post(SM02ViewModel model)
        {
            try
            {
                service.addSM02(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
        public HttpResponseMessage Put(SM02ViewModel model)
        {
            try
            {
                    service.saveSM02(model);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
        }
        public HttpResponseMessage Delete(string year, string org, string dept, string soft_id, int detail_id)
        {
            try
            {
                service.deleteSM02(year, org, dept, soft_id, detail_id);
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
