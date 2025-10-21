using GroupCalendar.Core.Data;
using GroupCalendar.Core.Entity;
using System;
using System.Threading.Tasks;

namespace GroupCalendar.Core.Repository
{
    public interface IMemberRepository : IDisposable
    {
        public Task SaveAsync(MemberRequest member);


        public Task<Member> FindByIdAsync(string memberId);

        public Task<Member> FindByloginIdAsync(string loginId);
    }
}
