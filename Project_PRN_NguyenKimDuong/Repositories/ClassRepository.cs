using BusinessObjects.Models;
using DataAccessLayer;

namespace Repositories
{
    public class ClassRepository : IClassRepository
    {
        public void AddClass(Class classObj)
            => ClassDAO.AddClass(classObj);

        public void DeleteClass(int classId)
            => ClassDAO.DeleteClass(classId);

        public Class GetClassByClassId(int classId)
            => ClassDAO.GetClassByClassId(classId);

        public List<Class> GetClasses()
            => ClassDAO.GetClasses();

        public List<Class> GetClassesByTrainerId(int trainerId)
            => ClassDAO.GetClassesByTrainerId(trainerId);
        public void UpdateClass(Class classObj)
            => ClassDAO.UpdateClass(classObj);

    }
}
