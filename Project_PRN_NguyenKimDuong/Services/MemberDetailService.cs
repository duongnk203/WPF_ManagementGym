using BusinessObjects.Models;
using Repositories;

namespace Services
{
    public class MemberDetailService : IMemberDetailService
    {
        private readonly IMemberDetailRepository iMemberDetailRepository;
        public MemberDetailService()
        {
            iMemberDetailRepository = new MemberDetailRepository();
        }
        public void AddMemberDetail(MemberDetail memberDetail)
            => iMemberDetailRepository.AddMemberDetail(memberDetail);

        public void DeleteMemberDetail(int id)
            => iMemberDetailRepository.DeleteMemberDetail(id);

        public MemberDetail GetMemberDetail(int id)
            => iMemberDetailRepository.GetMemberDetail(id);

        public List<MemberDetail> GetMemberDetails()
            => iMemberDetailRepository.GetMemberDetails();

        public void UpdateMemberDetail(MemberDetail memberDetail)
            => iMemberDetailRepository.UpdateMemberDetail(memberDetail);
    }
}
