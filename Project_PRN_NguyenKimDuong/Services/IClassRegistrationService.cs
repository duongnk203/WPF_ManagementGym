using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IClassRegistrationService
    {
        List<ClassRegistration> GetClassRegistrations();
        ClassRegistration GetClassRegistrationByClassRegistrationId(int classRegistrationId);
        void AddClassRegistration(ClassRegistration classRegistration);
        void UpdateClassRegistration(ClassRegistration classRegistration);
        void DeleteClassRegistration(int classRegistrationId);
        List<ClassRegistration> GetClassRegistrationsByMemberId(int memberId);
        List<ClassRegistration> GetClassRegistrationsByClassId(int classId);
    }
}
