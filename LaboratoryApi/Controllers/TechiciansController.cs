using LaboratoryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LaboratoryApi.Controllers
{
    public class TechiciansController : ApiController
    {
        public IEnumerable<Technicians> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Technicians.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var data = context.Technicians.FirstOrDefault(t => t.Technician_id == id);

                if (data != null)
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Technician with id = " + id.ToString() + " not found");
            }
        }
    }
}
