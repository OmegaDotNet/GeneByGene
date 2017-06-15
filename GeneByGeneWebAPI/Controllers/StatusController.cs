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
    public class StatusesController : Controller
    {
        // GET gbg/statuses
        [HttpGet]
        public IEnumerable<Status> Get()
        {
            return getStatuses();
        }

        // GET gbg/statuses/5
        [HttpGet("{id:int}")]
        public Status Get(int id)
        {
            return getStatuses().Where(s => s.StatusId == id).FirstOrDefault();
        }

        // POST gbg/statuses
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT gbg/statuses/5
        [HttpPut("{id:int}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE gbg/statuses/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        public bool AddStatus([FromBody]Status status)
        {
            try
            {
                int nextStatus = getStatuses().Max(s => s.StatusId) + 1;

                var fileStream = new FileStream(@"./ExternalResources/Statuses.txt", FileMode.Open, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    sw.WriteLine($"{nextStatus},{status.status}");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private List<Status> getStatuses()
        {
            List<Status> statuses = new List<Status>();

            Boolean header = true;
            var fileStream = new FileStream(@"./ExternalResources/Statuses.txt", FileMode.Open, FileAccess.Read);
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

                    statuses.Add(new Status { StatusId = int.Parse(line.Split(',')[0]), status = line.Split(',')[1] });
                }
            }
            return statuses;
        }
    }
}