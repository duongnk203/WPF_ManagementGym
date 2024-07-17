using BusinessObjects.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ScheduleRepository : IScheduleRepostitory
    {
        public List<Schedule> GetSchedules()
            => ScheduleDAO.GetAllSchedules();
    }
}
