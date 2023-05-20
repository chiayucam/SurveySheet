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

        public async Task CreateUserAsync(User user)
        {
            try
            {
                var sql =
                @"
                INSERT INTO [dbo].[User] (Role, Username, PasswordHash)
                VALUES (@Role, @Username, @PasswordHash)
                ";

                using var conn = new SqlConnection(ConnectionString);

                var param = new { Role = user.Role, Username = user.Username, PasswordHash = user.PasswordHash };
                await conn.ExecuteAsync(sql, param);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    throw new InvalidOperationException("Username used");
                }

                throw ex;
            }
        }

        public async Task<User> GetUserAsync(string username)
        {
            var sql =
                @"
                SELECT Id, Role, Username, PasswordHash
                FROM [dbo].[User]
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
