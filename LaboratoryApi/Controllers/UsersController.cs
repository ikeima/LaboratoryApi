using LaboratoryDataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.Net.Http;
using System.Net;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace LaboratoryApi.Controllers
{  
    public class UsersController : ApiController
    {
        // GET запрос для получения списка Пользователей
        
        public IEnumerable<Users> Get() 
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var users = context.Users.Include(u => u.Roles).ToList();
                return users;
            }
        }

        // GET запрос для получения конкретного пользователя по ID
        [Microsoft.AspNetCore.Mvc.Route("/users/Get/1.json")]
        public HttpResponseMessage Get(int id)
        {
            using (LaboratoryEntities context = new LaboratoryEntities())
            {
                var user = context.Users.FirstOrDefault(u => u.User_id == id);

                if (user != null)
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with ID = " + id.ToString() +" not found");
            }
        }

        // POST запрос для вставки записи в таблицу
        public HttpResponseMessage Post([System.Web.Http.FromBody] Users user)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    context.Users.Add(user);
                    context.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new System.Uri(Request.RequestUri + user.User_id.ToString());

                    return message;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        // PUT запрос для замены/редактирования записи в таблице
        public HttpResponseMessage Put(int id, [System.Web.Http.FromBody] Users user)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    var editingUser = context.Users.FirstOrDefault(u => u.User_id == id);

                    if (editingUser != null)
                    {
                        editingUser.Login = user.Login;
                        editingUser.Password = user.Password;
                        editingUser.First_name = user.First_name;
                        editingUser.Last_name = user.Last_name;
                        editingUser.Patronymic = user.Patronymic;
                        editingUser.Last_enter_date_time = user.Last_enter_date_time;
                        editingUser.Last_logout_date_time = user.Last_logout_date_time;
                        editingUser.IP = user.IP;

                        context.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, editingUser);
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

        // DELETE запрос для удаления записи из таблицы
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (LaboratoryEntities context = new LaboratoryEntities())
                {
                    var user = context.Users.FirstOrDefault(u => u.User_id == id);

                    if (user != null)
                    {
                        context.Users.Remove(user);
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
