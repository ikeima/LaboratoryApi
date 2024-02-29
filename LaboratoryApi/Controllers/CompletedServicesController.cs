using LaboratoryDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LaboratoryApi.Controllers
{
    public class CompletedServicesController : ApiController
    {
        public IEnumerable<Completed_service> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Completed_service.ToList();
            }
        }

        // GET запрос для получения конкретного пользователя по ID      
        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var service = context.Completed_service.FirstOrDefault(u => u.Completed_service_id == id);

                if (service != null)
                    return Request.CreateResponse(HttpStatusCode.OK, service);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Completed service with ID = " + id.ToString() + " not found");
            }
        }

        // POST запрос для вставки записи в таблицу
        public HttpResponseMessage Post([FromBody] Completed_service service)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    context.Completed_service.Add(service);
                    context.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, service);
                    message.Headers.Location = new System.Uri(Request.RequestUri + service.Completed_service_id.ToString());

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
