using BusinessObjects.Models;
using DataAccessLayer;

namespace Repositories
{
    public class ClassRegistrationRepository : IClassRegistrationRepository
    {
        public List<ClassRegistration> GetClassRegistrations()
            => ClassRegistrationDAO.GetRegistrations();

        public ClassRegistration GetClassRegistrationByClassRegistrationId(int classRegistrationId)
            => ClassRegistrationDAO.GetRegistrationByClassRegistrationId(classRegistrationId);

        public void AddClassRegistration(ClassRegistration classRegistration)
            => ClassRegistrationDAO.AddClassRegistration(classRegistration);
        public void UpdateClassRegistration(ClassRegistration classRegistration)
            => ClassRegistrationDAO.UpdateClassRegistration(classRegistration);
        public void DeleteClassRegistration(int classRegistrationId)
            => ClassRegistrationDAO.DeleteClassRegistration(classRegistrationId);

        public List<ClassRegistration> GetClassRegistrationsByMemberId(int memberId)
            => ClassRegistrationDAO.GetClassRegistrationsByMemberId(memberId);

        public List<ClassRegistration> GetClassRegistrationsByClassId(int classId)
            => ClassRegistrationDAO.GetClassRegistrationsByClassId(classId);
    }
}
