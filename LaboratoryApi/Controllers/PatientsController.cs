using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using LaboratoryDataAccess;

namespace LaboratoryApi.Controllers
{
    public class PatientsController : ApiController
    {
        // GET запрос для получения всего списка 
        public IEnumerable<Patients> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Patients
                    .Include(p => p.Insurance_companies)
                    .Include(p => p.Patients_services)
                    .Include(p =>p.Orders)
                    .ToList();
            }
        }

        // GET запрос для получения конкретной записи по ID
        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var patient = context.Patients.Include(p => p.Insurance_companies).Where(p => p.Insurance_company_id == p.Insurance_companies.Insurance_company_id).FirstOrDefault(p => p.Patient_id == id);

                if (patient != null)
                    return Request.CreateResponse(HttpStatusCode.OK, patient);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient with ID = " + id.ToString() + " not found");
            }
        }

        // POST запрос для вставки записи в таблицу
        public HttpResponseMessage Post([FromBody] Patients patient)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    context.Patients.Add(patient);
                    context.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, patient);
                    message.Headers.Location = new Uri(Request.RequestUri + patient.Patient_id.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // PUT запрос для замены/редактирования записи в таблице
        public HttpResponseMessage Put(int id, [FromBody] Patients patient)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    var editingPatient = context.Patients.FirstOrDefault(u => u.Patient_id == id);

                    if (editingPatient != null)
                    {
                        editingPatient.Last_name = patient.Last_name;
                        editingPatient.First_name = patient.First_name;
                        editingPatient.Patronymic = patient.Patronymic;
                        editingPatient.Birth_date = patient.Birth_date;
                        editingPatient.Passport_number_series = patient.Passport_number_series;
                        editingPatient.Phone = patient.Phone;
                        editingPatient.Email = patient.Email;
                        editingPatient.Insurance_policy_number = patient.Insurance_policy_number;
                        editingPatient.Insurance_company_id = patient.Insurance_company_id;

                        context.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, editingPatient);
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

        // DELETE запрос для удаления записи из таблицы
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    var patient = context.Patients.FirstOrDefault(u => u.Patient_id == id);

                    if (patient != null)
                    {
                        context.Patients.Remove(patient);
                        context.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, patient);
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
