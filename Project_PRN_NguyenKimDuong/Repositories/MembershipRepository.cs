using BusinessObjects.Models;
using DataAccessLayer;

namespace Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        public Membership GetMembership(int id)
            =>MembershipDAO.GetMembership(id);

        public List<Membership> GetMemberships()
            => MembershipDAO.GetMemberShips();
    }
}
