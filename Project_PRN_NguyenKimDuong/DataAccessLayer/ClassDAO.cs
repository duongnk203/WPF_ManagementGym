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
    }
}
