using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IMemberDetailRepository
    {
        List<MemberDetail> GetMemberDetails();
        MemberDetail GetMemberDetail(int id);
        void AddMemberDetail(MemberDetail memberDetail);
        void UpdateMemberDetail(MemberDetail memberDetail);
        void DeleteMemberDetail(int id);
    }
}
