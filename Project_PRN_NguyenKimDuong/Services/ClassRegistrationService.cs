using BusinessObjects.Models;
using Repositories;

namespace Services
{
    public class ClassRegistrationService : IClassRegistrationService
    {
        private readonly IClassRegistrationRepository iClassRegistrationRepository;

        public ClassRegistrationService()
        {
            iClassRegistrationRepository = new ClassRegistrationRepository();
        }
        public List<ClassRegistration> GetClassRegistrations()
            => iClassRegistrationRepository.GetClassRegistrations();

        public ClassRegistration GetClassRegistrationByClassRegistrationId(int classRegistrationId)
            => iClassRegistrationRepository.GetClassRegistrationByClassRegistrationId(classRegistrationId);

        public void AddClassRegistration(ClassRegistration classRegistration)
            => iClassRegistrationRepository.AddClassRegistration(classRegistration);
        public void UpdateClassRegistration(ClassRegistration classRegistration)
            => iClassRegistrationRepository.UpdateClassRegistration(classRegistration);
        public void DeleteClassRegistration(int classRegistrationId)
            => iClassRegistrationRepository.DeleteClassRegistration(classRegistrationId);
        public List<ClassRegistration> GetClassRegistrationsByMemberId(int memberId)
            => iClassRegistrationRepository.GetClassRegistrationsByMemberId(memberId);
        public List<ClassRegistration> GetClassRegistrationsByClassId(int classId)
            => iClassRegistrationRepository.GetClassRegistrationsByClassId(classId);
    }
}
