using Dapper;
using GroupCalendar.Core.Data;
using GroupCalendar.Core.Entity;
using GroupCalendar.Core.Helpers;
using System;
using System.Threading.Tasks;
namespace GroupCalendar.Core.Repository
{
    public class LocalMemberRepository : IMemberRepository
    {
        private readonly TransactionManager _tm;
        private bool disposedValue;

        public LocalMemberRepository(TransactionManager tm)
        {
            _tm = tm;
        }

        public async Task SaveAsync(MemberRequest member)
        {
            string sql = @"
INSERT INTO Member (login_id, password, name)
VALUES(@LoginId, @Password, @Name);
";
            await _tm.Connection.ExecuteAsync(sql, member, _tm.Transaction);
            await _tm.CommitAsync();
        }

        public async Task<Member> FindByIdAsync(string memberId)
        {
            string sql = @"SELECT A.* FROM Member A WHERE A.member_id = @memberId";
            return await _tm.Connection.QueryFirstOrDefaultAsync<Member>(sql, new { memberId }, _tm.Transaction);
        }

        public async Task<Member> FindByloginIdAsync(string loginId)
        {
            string sql = @"SELECT A.* FROM Member A WHERE A.login_id = @loginId";
            return await _tm.Connection.QueryFirstOrDefaultAsync<Member>(sql, new { loginId }, _tm.Transaction);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~LocalMemberRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
