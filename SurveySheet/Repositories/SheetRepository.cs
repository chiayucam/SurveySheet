using Dapper;
using SurveySheet.Repositories.Interfaces;
using SurveySheet.Repositories.Models;
using System.Data.SqlClient;
using Z.Dapper.Plus;

namespace SurveySheet.Repositories
{
    public class SheetRepository : ISheetRepository
    {
        private readonly string ConnectionString;

        public SheetRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task AddItemsAsync(IEnumerable<AddItem> addItems)
        {
            DapperPlusManager.Entity<AddItem>().Table("Item");

            using var conn = new SqlConnection(ConnectionString);

            conn.BulkInsert(addItems);
        }

        public async Task<IEnumerable<Item>> GetInitialItemsAsync(int limit)
        {
            var sql =
                @"
                SELECT TOP(@limit) Id, Title
                FROM Item
                ORDER BY Id
                ";

            using var conn = new SqlConnection(ConnectionString);

            var param = new { Limit = limit };
            var items = await conn.QueryAsync<Item>(sql, param);

            return items;
        }

        public async Task<IEnumerable<Item>> GetNextItemsAsync(int limit, int nextCursor)
        {
            var sql =
                @"
                SELECT TOP(@limit) Id, Title
                FROM Item
                WHERE Id > @nextCursor
                ORDER BY Id
                ";

            using var conn = new SqlConnection(ConnectionString);

            var param = new { Limit = limit, NextCursor = nextCursor };
            var items = await conn.QueryAsync<Item>(sql, param);

            return items;
        }

        public async Task UpdateItemAysnc(Item item)
        {
            var sql =
                @"
                UPDATE Item
                SET Title = @Title
                WHERE Id = @Id
                ";

            using var conn = new SqlConnection(ConnectionString);
            var param = new {Title = item.Title, Id = item.Id};
            var rowsAffected = await conn.ExecuteAsync(sql, param);

            if (rowsAffected == 0)
            {
                throw new InvalidOperationException("Update item not found");
            }
        }
    }
}
