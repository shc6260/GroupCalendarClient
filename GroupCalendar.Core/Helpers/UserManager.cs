using GroupCalendar.Core.Entity;

namespace GroupCalendar.Core.Helpers
{
    public class UserManager
    {
        public static UserManager Instance { get; } = new UserManager();

        private UserManager() { }


        /// <summary>
        /// 현재 사용자 정보.
        /// </summary>
        private Member _current;

        /// <summary>
        /// 현재 로그인 사용자 반환.
        /// </summary>
        public Member GetCurrent() => _current;

        public void SetCurrentUser(Member member)
        {
            _current = member;
        }
    }
}
