using BusinessObjects.Models;
using DataAccessLayer;

namespace Repositories
{
    public class MemberDetailRepository : IMemberDetailRepository
    {
        public void AddMemberDetail(MemberDetail memberDetail)
            => MemberDetailDAO.AddMemberDetail(memberDetail);
        public void DeleteMemberDetail(int id)
            => MemberDetailDAO.DeleteMemberDetail(id);

        public MemberDetail GetMemberDetail(int id)
            => MemberDetailDAO.GetMemberDetailByMemberId(id);

        public List<MemberDetail> GetMemberDetails()
            => MemberDetailDAO.GetMemberDetails();

        public void UpdateMemberDetail(MemberDetail memberDetail)
            => MemberDetailDAO.UpdateMemberDetail(memberDetail);
    }
}
