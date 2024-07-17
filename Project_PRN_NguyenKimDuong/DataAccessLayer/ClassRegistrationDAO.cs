using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ClassRegistrationDAO
    {
        public static List<ClassRegistration> GetClassRegistrationsByClassId(int classId)
        {
            using (var context = new GymManagementContext())
            {
                return context.ClassRegistrations.Where(cr => cr.ClassId == classId).ToList();
            }
        }

        public static List<ClassRegistration> GetRegistrations()
        {
            using var context = new GymManagementContext();
            return context.ClassRegistrations.ToList();
        }

        public static ClassRegistration GetRegistrationByClassRegistrationId(int classRegistrationId)
        {
            using var context = new GymManagementContext();
            return context.ClassRegistrations.FirstOrDefault(cr => cr.RegistrationId == classRegistrationId);
        }

        public static void AddClassRegistration(ClassRegistration classRegistration)
        {
            using var context = new GymManagementContext();
            try
            {
                if (classRegistration != null)
                {
                    var classRegistrations = context.ClassRegistrations.Where(cr => cr.ClassId == classRegistration.ClassId && cr.MemberId == classRegistration.MemberId).ToList();
                    if (classRegistrations.Count > 0)
                    {
                        throw new DataAccessException("This member has already registered for this class");
                    }
                }
                context.ClassRegistrations.Add(classRegistration);
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void UpdateClassRegistration(ClassRegistration classRegistration)
        {
            using var context = new GymManagementContext();
            try
            {
                var classRegistrationToUpdate = context.ClassRegistrations.FirstOrDefault(cr => cr.RegistrationId == classRegistration.RegistrationId);
                if (classRegistrationToUpdate != null)
                {
                    if (classRegistrationToUpdate.ClassId != classRegistration.ClassId || classRegistration.MemberId != classRegistrationToUpdate.MemberId)
                    {
                        if (IsMemberRegisteredForClass(classRegistration.ClassId, classRegistration.MemberId))
                        {
                            throw new DataAccessException("This member has already registered for this class");
                        }
                        else
                        {
                            classRegistrationToUpdate.ClassId = classRegistration.ClassId;
                            classRegistrationToUpdate.MemberId = classRegistration.MemberId;
                            classRegistrationToUpdate.RegistrationDate = classRegistration.RegistrationDate;
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        classRegistrationToUpdate.RegistrationDate = classRegistration.RegistrationDate;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static bool IsMemberRegisteredForClass(int classId, int memberId)
        {
            using var context = new GymManagementContext();
            var classRegistrations = context.ClassRegistrations.Where(cr => cr.ClassId == classId && cr.MemberId == memberId).ToList();
            return classRegistrations.Count > 0;
        }

        public static void DeleteClassRegistration(int classRegistrationId)
        {
            using var context = new GymManagementContext();
            var classRegistrationToDelete = context.ClassRegistrations.FirstOrDefault(cr => cr.RegistrationId == classRegistrationId);
            if (classRegistrationToDelete != null)
            {
                context.ClassRegistrations.Remove(classRegistrationToDelete);
                context.SaveChanges();
            }
        }

        public static List<ClassRegistration> GetClassRegistrationsByMemberId(int memberId)
        {
            using var context = new GymManagementContext();
            return context.ClassRegistrations.Where(cr => cr.MemberId == memberId).ToList();
        }
    }
}
