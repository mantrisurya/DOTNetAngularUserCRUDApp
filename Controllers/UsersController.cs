using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LNUserListingApp.Data;
using LNUserListingApp.Models;
using Microsoft.Extensions.Configuration;
using LNUserListingApp.DAL;
using System.Data;

namespace LNUserListingApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService userService;
        private readonly LogException objLogException;

        public UsersController(UsersService service)
        {
            userService = service;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            List<User> usersList = new List<User>();
            try
            {
                DataTable usersDT = userService.fetchusers(0,0);
                usersList = (from DataRow dr in usersDT.Rows
                                select new User()
                                {
                                    UserID = Convert.ToInt32(dr["UserID"]),
                                    Name = Convert.ToString(dr["Name"]),
                                    Email = Convert.ToString(dr["Email"]),
                                    MobileNumber = Convert.ToString(dr["MobileNumber"]),
                                    status = Convert.ToString(usersDT.Rows[0]["Status"])
            }).ToList();
                return usersList.ToArray();
            }
            catch (Exception e)
            {
                this.objLogException.LogExceptionDetails(e, e.Source, 0, "GetUser");
                return usersList;
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            User user = new User();
            try
            {
                DataTable usersDT = userService.fetchusers(id,0);
                if (usersDT.Rows.Count > 0)
                {
                    user.UserID = Convert.ToInt32(usersDT.Rows[0]["UserID"]);
                    user.Name = Convert.ToString(usersDT.Rows[0]["Name"]);
                    user.Email = Convert.ToString(usersDT.Rows[0]["Email"]);
                    user.MobileNumber = Convert.ToString(usersDT.Rows[0]["MobileNumber"]);
                    user.status = Convert.ToString(usersDT.Rows[0]["Status"]);
                }
                return user;
            }
            catch (Exception e)
            {
                this.objLogException.LogExceptionDetails(e, e.Source, 0, "GetUser");
                return user;
            }            
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public int PutUser( int userId, User user)
        {
            int result = 0;
            try
            {
                result = userService.updateUser(user, 0, userId);
                return result;
            }
            catch (Exception e)
            {
                this.objLogException.LogExceptionDetails(e, e.Source, 0, "PutUser");

            }

            return result;
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public int PostUser(User user)
        {
            int result = 0;
            try
            {
                result = userService.insertUser(user, 0);
                return result;
            }
            catch (Exception e)
            {
                this.objLogException.LogExceptionDetails(e, e.Source, 0, "PostUser");

            }

            return result;
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public int DeleteUser(int id)
        {
            int result = 0;
            try
            {
                result = userService.removeUser(id);
                return result;
            }
            catch (Exception e)
            {
                this.objLogException.LogExceptionDetails(e, e.Source, 0, "PostUser");

            }

            return result;
        }
    }
}
