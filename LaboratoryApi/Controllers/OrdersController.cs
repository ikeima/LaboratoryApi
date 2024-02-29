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
    public class OrdersController : ApiController
    {
        public IEnumerable<Orders> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Orders
                    .Include(o => o.Patients)
                    .ToList();
            }
        }

        // GET запрос для получения конкретного пользователя по ID      
        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var order = context.Orders.FirstOrDefault(u => u.Order_id == id);

                if (order != null)
                    return Request.CreateResponse(HttpStatusCode.OK, order);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order with ID = " + id.ToString() + " not found");
            }
        }

        // POST запрос для вставки записи в таблицу
        public HttpResponseMessage Post([FromBody] Orders order)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, order);
                    message.Headers.Location = new System.Uri(Request.RequestUri + order.Order_id.ToString());

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
