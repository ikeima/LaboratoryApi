using LaboratoryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LaboratoryApi.Controllers
{
    public class AnalyzersController : ApiController
    {
        public IEnumerable<Analyzers> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Analyzers.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var data = context.Analyzers.FirstOrDefault(a => a.Analyzer_id == id);

                if (data != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Analyzer with id = " + id.ToString() + " not found");
            }
        }
    }
}
