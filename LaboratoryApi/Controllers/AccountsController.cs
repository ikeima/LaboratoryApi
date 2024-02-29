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
    public class AccountsController : ApiController
    {
        // GET запрос для получения списка Пользователей
        public IEnumerable<Accounts> Get()
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                return context.Accounts
                    .Include(a => a.Accountants)
                    .Include(a => a.Insurance_companies)
                    .Include(a => a.Accountants.Users)
                    .ToList();
            }
        }

        // GET запрос для получения конкретного пользователя по ID      
        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var account = context.Accounts.FirstOrDefault(u => u.Account_id == id);

                if (account != null)
                    return Request.CreateResponse(HttpStatusCode.OK, account);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with ID = " + id.ToString() + " not found");
            }
        }

        // POST запрос для вставки записи в таблицу
        public HttpResponseMessage Post([FromBody] Accounts account)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    context.Accounts.Add(account);
                    context.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, account);
                    message.Headers.Location = new System.Uri(Request.RequestUri + account.Account_id.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // PUT запрос для замены/редактирования записи в таблице
        public HttpResponseMessage Put(int id, [FromBody] Accounts account)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    var editingAccount = context.Accounts.FirstOrDefault(u => u.Account_id == id);

                    if (editingAccount != null)
                    {
                        editingAccount.Accountant_id = account.Accountant_id;
                        editingAccount.Insurance_company_id = account.Insurance_company_id;
                        editingAccount.Number = account.Number;
                        
                        context.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, editingAccount);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Account with ID = " + id.ToString() + " not found");
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
                    var user = context.Accounts.FirstOrDefault(u => u.Account_id == id);

                    if (user != null)
                    {
                        context.Accounts.Remove(user);
                        context.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, user);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with ID = " + id.ToString() + " not found");
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
