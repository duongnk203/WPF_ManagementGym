using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MembershipDAO
    {
        public static List<Membership> GetMemberShips()
        {
            var listMemberships = new List<Membership>();
            using var context = new GymManagementContext();
            listMemberships = context.Memberships.ToList();
            return listMemberships;
        }

        public static Membership GetMembership(int id)
        {
            using var context = new GymManagementContext();
            var membership = context.Memberships.FirstOrDefault(m => m.MembershipId == id);
            return membership;
        }

    }
}
