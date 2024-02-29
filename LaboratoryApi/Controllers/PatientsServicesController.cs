using LaboratoryDataAccess;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LaboratoryApi.Controllers
{
    public class PatientsServicesController : ApiController
    {
        public IEnumerable<Patients_services> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Patients_services
                    .Include(ps => ps.Patients)
                    .Include(ps => ps.Services)
                    .ToList();
            }
        }

        // GET запрос для получения конкретной записи по ID
        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var patientService = context.Patients_services.FirstOrDefault(p => p.Patients_services_id == id);

                if (patientService != null)
                    return Request.CreateResponse(HttpStatusCode.OK, patientService);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient service with ID = " + id.ToString() + " not found");
            }
        }

        // POST запрос для вставки записи в таблицу
        public HttpResponseMessage Post([FromBody] Patients_services patientService)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    context.Patients_services.Add(patientService);
                    context.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, patientService);
                    message.Headers.Location = new System.Uri(Request.RequestUri + patientService.Patients_services_id.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // DELETE запрос для удаления записи из таблицы
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    var patientService = context.Patients_services.FirstOrDefault(p  => p.Patients_services_id== id);

                    if (patientService != null)
                    {
                        context.Patients_services.Remove(patientService);
                        context.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, patientService);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient with ID = " + id.ToString() + " not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
