using LaboratoryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LaboratoryApi.Controllers
{
    public class AnalyzersDataController : ApiController
    {
        public IEnumerable<Analyzers_data> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Analyzers_data.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var data = context.Analyzers_data.FirstOrDefault(d => d.Analyzer_id == id);

                if (data != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Data with id = " + id.ToString() + " not found");
            }    
        }

        public HttpResponseMessage Post([FromBody]Analyzers_data data)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    context.Analyzers_data.Add(data);
                    context.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.OK, data);
                    message.Headers.Location = new Uri(Request.RequestUri + data.Analyzer_id.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
