using LaboratoryDataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LaboratoryApi.Controllers
{
    public class ServicesController : ApiController
    {
        public IEnumerable<Services> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Services.ToList();
            }
        }

        public HttpResponseMessage Get(int code)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var data = context.Services.FirstOrDefault(s => s.Service_code == code);

                if (data != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Service with code = " + code.ToString() + " not found");
            }
        }
    }
}
