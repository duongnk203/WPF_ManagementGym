using BusinessObjects.Models;
using Repositories;

namespace Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository iMembershipRepository;

        public MembershipService()
        {
            iMembershipRepository = new MembershipRepository();
        }
        public List<Membership> GetMemberships()
            => iMembershipRepository.GetMemberships();

        public Membership GetMembership(int id)
            => iMembershipRepository.GetMembership(id);
    }
}
