using BusinessObjects.Models;
using DataAccessLayer;

namespace Repositories
{
    public class ClassRepository : IClassRepository
    {
        public Class GetClassByClassId(int classId)
            => ClassDAO.GetClassByClassId(classId);

        public List<Class> GetClasses()
            => ClassDAO.GetClasses();

        public List<Class> GetClassesByTrainerId(int trainerId)
            => ClassDAO.GetClassesByTrainerId(trainerId);
    }
}
