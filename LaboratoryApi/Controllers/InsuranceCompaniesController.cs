using LaboratoryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LaboratoryApi.Controllers
{
    public class InsuranceCompaniesController : ApiController
    {
        public IEnumerable<Insurance_companies> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Insurance_companies.ToList();
            }
        }

        // GET запрос для получения конкретного пользователя по ID      
        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var company = context.Insurance_companies.FirstOrDefault(u => u.Insurance_company_id == id);

                if (company != null)
                    return Request.CreateResponse(HttpStatusCode.OK, company);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Insurance company with ID = " + id.ToString() + " not found");
            }
        }

       
    }
}
