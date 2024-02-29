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
    public class HistoryController : ApiController
    {
        public IEnumerable<History> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.History.Include(h => h.Users).ToList();
            }
        }

        // GET запрос для получения конкретного пользователя по ID
        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var history = context.History.FirstOrDefault(u => u.History_id == id);

                if (history != null)
                    return Request.CreateResponse(HttpStatusCode.OK, history);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "History with ID = " + id.ToString() + " not found");
            }
        }

        // POST запрос для вставки записи в таблицу
        public HttpResponseMessage Post([FromBody] History history)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    context.History.Add(history);
                    context.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, history);
                    message.Headers.Location = new System.Uri(Request.RequestUri + history.History_id.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // PUT запрос для замены/редактирования записи в таблице
        public HttpResponseMessage Put(int id, [FromBody] History history)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    var editingHistory = context.History.FirstOrDefault(u => u.History_id == id);

                    if (editingHistory != null)
                    {
                        editingHistory.User_id = history.User_id;
                        editingHistory.Enter_date_time = history.Enter_date_time;
                        editingHistory.Logout_date_time = history.Logout_date_time;
                        
                        context.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, editingHistory);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "History with ID = " + id.ToString() + " not found");
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
                    var history = context.History.FirstOrDefault(u => u.History_id == id);

                    if (history != null)
                    {
                        context.History.Remove(history);
                        context.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, history);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "History with ID = " + id.ToString() + " not found");
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
