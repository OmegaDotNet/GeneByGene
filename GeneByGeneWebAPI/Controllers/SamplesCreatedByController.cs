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
    public class SamplesCreatedByController : Controller
    {
        // GET gbg/samplescreatedby/5
        [HttpGet("{id:int}")]
        public IEnumerable<FullSample> Get(int id)
        {
            return getAllSamples().Where(fs => fs.CreatedById == id);
        }

        // GET gbg/samplescreatedby/abc
        [Route("{user}")]
        [HttpGet]
        public IEnumerable<FullSample> GetByName(string user)
        {
            return getAllSamples().Where(fs => fs.CreatedBy.ToUpper().Contains(user.ToUpper()));
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