using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IClassRepository
    {
        List<Class> GetClassesByTrainerId(int trainerId);
        List<Class> GetClasses();
        Class GetClassByClassId(int classId);
        void AddClass(Class classObj);
        void UpdateClass(Class classObj);
        void DeleteClass(int classId);
    }
}
