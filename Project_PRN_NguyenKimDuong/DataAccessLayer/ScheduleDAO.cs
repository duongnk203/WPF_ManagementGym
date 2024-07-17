using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ScheduleDAO
    {
        public static List<Schedule> GetAllSchedules()
        {
            using (var context = new GymManagementContext())
            {
                return context.Schedules.ToList();
            }
        }

        public static Schedule GetScheduleById(int scheduleId)
        {
            using (var context = new GymManagementContext())
            {
                return context.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
            }
        }

        public static void AddSchedule(Schedule schedule)
        {
            using (var context = new GymManagementContext())
            {
                context.Schedules.Add(schedule);
                context.SaveChanges();
            }
        }

        public static void UpdateSchedule(Schedule schedule)
        {
            using (var context = new GymManagementContext())
            {
                var existingSchedule = context.Schedules.FirstOrDefault(s => s.ScheduleId == schedule.ScheduleId);
                if (existingSchedule != null)
                {
                    existingSchedule.ScheduleName = schedule.ScheduleName;
                    context.SaveChanges();
                }
            }
        }

        public static void DeleteSchedule(int scheduleId)
        {
            using (var context = new GymManagementContext())
            {
                var schedule = context.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
                if (schedule != null)
                {
                    context.Schedules.Remove(schedule);
                    context.SaveChanges();
                }
            }
        }
    }
}
