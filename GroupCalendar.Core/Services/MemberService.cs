using GroupCalendar.Core.Data;
using GroupCalendar.Core.Repository;
using System;
using System.Threading.Tasks;


namespace GroupCalendar.Core.Services
{
    public class MemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task AddMemberAsync(MemberRequest req)
        {
            using (var scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
            {
                // 트랜잭션이 필요한 쓰기 작업만 TransactionManager를 사용
                try
                {
                    // 중복 아이디 확인
                    var existingMember = await _memberRepository.FindByloginIdAsync(req.LoginId);
                    if (existingMember != null)
                    {
                        throw new InvalidOperationException("이미 존재하는 회원 ID입니다.");
                    }

                    req.Password = BCrypt.Net.BCrypt.HashPassword(req.Password);
                    await _memberRepository.SaveAsync(req);
                }
                catch (Exception)
                {
                    throw;
                }

                // 모든 작업이 성공했으므로 커밋
                scope.Complete();
            }
        }
    }
}
