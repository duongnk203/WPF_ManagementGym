using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ClassDAO
    {
        public static List<Class> GetClassesByTrainerId(int trainerId)
        {
            using var context = new GymManagementContext();
            var classes = context.Classes.Where(c => c.TrainerId == trainerId).ToList();
            return classes;
        }

        public static List<Class> GetClasses()
        {
           using var context = new GymManagementContext();
            var classes = context.Classes.ToList();
            return classes;
        }

        public static Class GetClassByClassId(int classId)
        {
            var classSelected = new Class();
            using var context = new GymManagementContext();
            classSelected = context.Classes.Where(c => c.ClassId == classId).FirstOrDefault();
            return classSelected;
        }

        private static bool IsExistClassByScheduleId(int scheduleId)
        {
            using var context = new GymManagementContext();
            var classObj = context.Classes.FirstOrDefault(c => c.ScheduleId == scheduleId);
            if (classObj != null)
            {
                return true;
            }
            return false;
        }

        public static void AddClass(Class classObj)
        {
            using var context = new GymManagementContext();
            try
            {
                if (IsExistClassByScheduleId(classObj.ScheduleId))
                    throw new DataAccessException("Shedule has already class in the system!");
                context.Classes.Add(classObj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public static void UpdateClass(Class classObj)
        {
            using var context = new GymManagementContext();
            try
            {
                if (IsExistClassByScheduleId(classObj.ScheduleId))
                    throw new DataAccessException("Shedule has already class in the system!");
                context.Classes.Update(classObj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void DeleteClass(int classId)
        {
            using var context = new GymManagementContext();
            var classObj = context.Classes.FirstOrDefault(c => c.ClassId == classId);
            if (classObj != null)
            {
                classObj.Status = false;
                context.Classes.Update(classObj);
                context.SaveChanges();
            }
        }
    }
}
