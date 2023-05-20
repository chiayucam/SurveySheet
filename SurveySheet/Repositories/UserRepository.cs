using Dapper;
using SurveySheet.Repositories.Interfaces;
using SurveySheet.Repositories.Models;
using System.Data.SqlClient;

namespace SurveySheet.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string ConnectionString;

        public UserRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<User> GetUserAsync(string username)
        {
            var sql =
                @"
                SELECT Id, Role, Username, PasswordHash
                FROM User
                WHERE Username = @Username
                ";

            using var conn = new SqlConnection(ConnectionString);

            var param = new { Username = username };
            var users = await conn.QueryAsync<User>(sql, param);

            var user = users.Single();

            return user;
        }
    }
}
