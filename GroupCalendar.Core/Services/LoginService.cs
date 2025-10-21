using GroupCalendar.Core.Entity;
using GroupCalendar.Core.Repository;
using System;
using System.Threading.Tasks;

namespace GroupCalendar.Core.Services
{
    public class LoginService
    {
        private readonly IMemberRepository _memberRepository;

        public LoginService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Member> LoginAsync(string loginId, string password)
        {
            var member = await _memberRepository.FindByloginIdAsync(loginId);

            if (member == null)
                throw new InvalidOperationException("아이디를 확인해 주세요");

            if (BCrypt.Net.BCrypt.Verify(password, member.password) == false)
                throw new InvalidOperationException("비밀번호를 확인해 주세요");

            return member;
        }
    }
}
