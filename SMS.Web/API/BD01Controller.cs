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
    public class BD01Controller : ApiController
    {
        private BD01Service service;
        public BD01Controller()
        {
            service = new BD01Service();
        }
        public HttpResponseMessage Get()
        {
            try
            {
                BD01Service BD01 = new BD01Service();
                var bd01datas = BD01.Get();
                return Request.CreateResponse(HttpStatusCode.OK, bd01datas);
                        
            }         
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

        }
        //public HttpResponseMessage Post(SM01ViewModel model)
        //{
        //    try
        //    {
        //        service.addSM01(model);
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }
        //}     
        //public HttpResponseMessage Put(SM01ViewModel model)
        //{
        //    try
        //    {
        //        service.saveSM01(model);
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }
        //}
        //public HttpResponseMessage Delete(string year, string org, string dept, string soft_id)
        //{
        //    try
        //    {
        //        service.deleteSM01(year,org,dept,soft_id);
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
        //    }
        //}

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
