using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using GeneByGeneWebAPI.Models;
using GeneByGeneWebAPI.ViewModels;
using System.Linq;

namespace GeneByGeneWebAPI.Controllers
{
    [Route("gbg/[controller]")]
    public class SamplesController : Controller
    {
        // GET gbg/samples
        //[HttpGet]
        //public IEnumerable<Sample> Get()
        //{
        //    //return getSamples();
        //    return getAllSamples();
        //}

        // GET gbg/samples
        [HttpGet]
        public IEnumerable<FullSample> Get()
        {
            //return getSamples();
            return getAllSamples();
        }

        // GET gbg/samples/5
        //[HttpGet("{id:int}")]
        //public Sample Get(int id)
        //{
        //    return getSamples().Where(s => s.SampleId == id).FirstOrDefault();
        //}

        // GET gbg/samples/5

        [HttpGet("{id:int}")]
        public FullSample Get(int id)
        {
            return getAllSamples().Where(s => s.SampleId == id).FirstOrDefault();
        }

        // POST gbg/samples
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT gbg/samples/5
        //[HttpPut("{id:int}")]
        //public void Put(int id, [FromBody]string barcode, [FromBody]DateTime createdAt, [FromBody]int createdBy, [FromBody]int statusId)
        //{
        //    try
        //    {
        //        int nextSample = getSamples().Max(s => s.SampleId) + 1;

        //        var fileStream = new FileStream(@"./ExternalResources/Samples.txt", FileMode.Open, FileAccess.Write);
        //        using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
        //        {
        //            sw.WriteLine($"{nextSample.ToString()},{barcode},{createdAt},{createdBy.ToString()},{statusId.ToString()}");
        //        }
        //        //return true;
        //    }
        //    catch
        //    {
        //        //return false;
        //    }
        //}

        // GET gbg/samples/abc
        [Route("{addsample}")]
        [HttpGet]
        public void AddSample(string barcode, string createdAt, string createdBy, string statusId)
        {
            try
            {
                int nextSample = getSamples().Max(s => s.SampleId) + 1;

                var fileStream = new FileStream(@"./ExternalResources/Samples.txt", FileMode.Append, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    sw.WriteLine($"{nextSample.ToString()},{barcode},{createdAt},{createdBy.ToString()},{statusId.ToString()}");
                }
            }
            catch
            {                
            }
        }

        // DELETE gbg/samples/5
        [HttpDelete("{id:int}")]
        public string Delete(int id)
        {
            return "delete";
        }

        private List<Sample> getSamples()
        {
            List<Sample> samples = new List<Sample>();

            Boolean header = true;
            var fileStream = new FileStream(@"./ExternalResources/Samples.txt", FileMode.Open, FileAccess.Read);
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

                    samples.Add(new Sample { SampleId = int.Parse(line.Split(',')[0]), Barcode = line.Split(',')[1], CreatedAt = DateTime.Parse(line.Split(',')[2]), CreatedBy = int.Parse(line.Split(',')[3]), StatusId = int.Parse(line.Split(',')[4]) });
                }
            }
            return samples;
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

        [HttpPost]
        public bool AddSample([FromBody]Sample sample)
        {
            try
            {
                int nextSample = getSamples().Max(s => s.SampleId) + 1;

                var fileStream = new FileStream(@"./ExternalResources/Samples.txt", FileMode.Open, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    sw.WriteLine($"{nextSample.ToString()},{sample.Barcode},{sample.CreatedAt.ToString("yyyy-MM-dd")},{sample.CreatedBy.ToString()},{sample.StatusId.ToString()}");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private IEnumerable<FullSample> getAllSamples()
        {
            IEnumerable<FullSample> fullSamples;

            List<Sample> samples = getSamples();
            List<Status> statuses = getStatuses();
            List<User> users = getUsers();

            fullSamples = from s in samples
                          join st in statuses on s.StatusId equals st.StatusId
                          join u in users on s.CreatedBy equals u.UserId
                          select new FullSample
                          {
                              SampleId = s.SampleId,
                              Barcode = s.Barcode,
                              CreatedAt = s.CreatedAt.ToString("yyyy-MM-dd"),
                              CreatedById = u.UserId,
                              CreatedBy = $"{u.FirstName} {u.LastName}",
                              StatusId = st.StatusId,
                              Status = st.status
                          };

            return fullSamples;
        }

    }    
}