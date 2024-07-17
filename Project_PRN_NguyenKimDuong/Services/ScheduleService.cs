using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepostitory iScheduleRepository;
        public ScheduleService()
        {

           iScheduleRepository = new ScheduleRepository();
        }
        public List<Schedule> GetSchedules()
            => iScheduleRepository.GetSchedules();
    }
}
