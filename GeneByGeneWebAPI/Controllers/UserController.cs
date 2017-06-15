using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using GeneByGeneWebAPI.Models;
using System.Linq;

namespace GeneByGeneWebAPI.Controllers
{
    [Route("gbg/[controller]")]
    public class UsersController : Controller
    {
        // GET gbg/users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return getUsers();
        }

        // GET gbg/users/5
        [HttpGet("{id:int}")]
        public User Get(int id)
        {
            return getUsers().Where(s => s.UserId == id).FirstOrDefault();
        }

        // POST gbg/users
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT gbg/users/?userid=10&firstname=hector&lastname=sanchez
        [HttpPut("{id:int}")]
        public void Put(int id, [FromBody]string valu)
        {
        }

        // DELETE gbg/users/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
        }

        [HttpPost]        
        public bool AddUser([FromBody]User user)
        {
            try
            {
                int nextUser = getUsers().Max(u => u.UserId) + 1;

                var fileStream = new FileStream(@"./ExternalResources/Users.txt", FileMode.Open, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    sw.WriteLine($"{nextUser},{user.FirstName},{user.LastName}");
                }
                return true;
            }
            catch {
                return false;
            }
        }

        private List<User> getUsers()
        {
            List<User> users = new List<User>();

            Boolean header = true;
            var fileStream = new FileStream(@"./ExternalResources/Users.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (header)
                    {
                        header = !header;
                        continue;
                    }

                    users.Add(new User { UserId = int.Parse(line.Split(',')[0]), FirstName = line.Split(',')[1], LastName = line.Split(',')[2] });
                }
            }
            return users;
        }
    }
}