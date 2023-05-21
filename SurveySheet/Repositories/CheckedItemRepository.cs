using Dapper;
using SurveySheet.Repositories.Interfaces;
using System.Data.SqlClient;

namespace SurveySheet.Repositories
{
    public class CheckedItemRepository : ICheckedItemRepository
    {
        private readonly string ConnectionString;

        public CheckedItemRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task CheckItemAsync(int userId, int itemId)
        {
            var sql =
                @"
                INSERT INTO [dbo].[UserCheckedItem] (UserId, ItemId)
                VALUES (@UserId, @ItemId)
                ";

            using var conn = new SqlConnection(ConnectionString);

            var param = new { UserId = userId, ItemId = itemId };
            await conn.ExecuteAsync(sql, param);
        }

        public async Task<IEnumerable<int>> GetCheckItemAsync(int userId, int start, int end)
        {
            var sql =
                @"
                SELECT ItemId
                FROM [dbo].[UserCheckedItem]
                WHERE UserId = @UserId AND ItemId >= @StartItemId AND ItemId <= @EndItemId
                ORDER BY ItemId
                ";

            using var conn = new SqlConnection(ConnectionString);

            var param = new { UserId = userId, StartItemId = start, EndItemId = end };
            var checkedItemIds = await conn.QueryAsync<int>(sql, param);

            return checkedItemIds;
        }
    }
}
