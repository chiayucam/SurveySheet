using SurveySheet.Repositories.Models;

namespace SurveySheet.Repositories.Interfaces
{
    public interface ISheetRepository
    {
        public Task<IEnumerable<Item>> GetInitialItemsAsync(int limit);

        public Task<IEnumerable<Item>> GetNextItemsAsync(int limit, int nextCursor);
    }
}
