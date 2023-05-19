using SurveySheet.Repositories.Interfaces;
using SurveySheet.Repositories.Models;

namespace SurveySheet.Repositories
{
    public class SheetRepository : ISheetRepository
    {
        public async Task<IEnumerable<Item>> GetInitialItemsAsync(int limit)
        {
            return new List<Item>();
        }

        public async Task<IEnumerable<Item>> GetNextItemsAsync(int limit, int nextCursor)
        {
            return new List<Item>();
        }
    }
}
