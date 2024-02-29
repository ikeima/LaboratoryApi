using LaboratoryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LaboratoryApi.Controllers
{
    public class RolesController : ApiController
    {
        public IEnumerable<Roles> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Roles.ToList();
            }
        }

        public HttpResponseMessage Get(string code)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var data = context.Roles.FirstOrDefault(d => d.Role_code == code);

                if (data != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Role with code = " + code + " not found");
            }
        }
    }
}
