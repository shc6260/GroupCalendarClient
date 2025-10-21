namespace GroupCalendar.Core.Data
{
    public class MemberRequest
    {
        // 생성자 파라미터 이름을 속성과 동일하게 변경
        public MemberRequest(string loginId, string password, string name)
        {
            LoginId = loginId;
            Password = password;
            Name = name;
        }

        public string LoginId { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
    }
}
