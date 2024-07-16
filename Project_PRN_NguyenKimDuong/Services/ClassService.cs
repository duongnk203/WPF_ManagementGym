using BusinessObjects.Models;
using Repositories;

namespace Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository iClassRepository;
        public ClassService()
        {
            iClassRepository = new ClassRepository();
        }
        public List<Class> GetClasses()
            => iClassRepository.GetClasses();

        public List<Class> GetClassesByTrainerId(int trainerId)
            => iClassRepository.GetClassesByTrainerId(trainerId);

        public Class GetClassByClassId(int classId)
            => iClassRepository.GetClassByClassId(classId);
    }
}
