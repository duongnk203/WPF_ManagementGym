using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IMembershipService
    {
        List<Membership> GetMemberships();

        Membership GetMembership(int id);
    }
}
