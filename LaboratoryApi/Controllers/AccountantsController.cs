using LaboratoryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LaboratoryApi.Controllers
{
    public class AccountantsController : ApiController
    {
        public IEnumerable<Accountants> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Accountants.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var data = context.Accountants.FirstOrDefault(a => a.Accountant_id == id);

                if (data != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Accountant with id = " + id.ToString() + " not found");
            }
        }
    }
}
