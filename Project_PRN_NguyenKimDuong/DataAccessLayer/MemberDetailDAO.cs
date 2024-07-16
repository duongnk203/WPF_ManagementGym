using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class MemberDetailDAO
    {
        public static List<MemberDetail> GetMemberDetails()
        {
            var listMemberDetails = new List<MemberDetail>();
            using var context = new GymManagementContext();
            listMemberDetails = context.MemberDetails.ToList();
            return listMemberDetails;
        }

        public static MemberDetail GetMemberDetailByMemberId(int memberId)
        {
            using var context = new GymManagementContext();
            var memberDetail = context.MemberDetails.FirstOrDefault(x => x.MemberId == memberId);
            return memberDetail;
        }

        public static void AddMemberDetail(MemberDetail memberDetail)
        {
            using var context = new GymManagementContext();
            context.MemberDetails.Add(memberDetail);
            context.SaveChanges();
        }

        public static void UpdateMemberDetail(MemberDetail memberDetail)
        {
            using var context = new GymManagementContext();
            context.MemberDetails.Update(memberDetail);
            context.SaveChanges();
        }

        public static void DeleteMemberDetail(int memberId)
        {
            using var context = new GymManagementContext();
            var memberDetail = context.MemberDetails.FirstOrDefault(x => x.MemberId == memberId);
            if(memberDetail != null)
            {
                memberDetail.Status = false;
                context.MemberDetails.Update(memberDetail);
                context.SaveChanges();
            }
        }
    }
}
