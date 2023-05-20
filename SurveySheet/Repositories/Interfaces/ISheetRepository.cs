using SurveySheet.Repositories.Models;

namespace SurveySheet.Repositories.Interfaces
{
    public interface ISheetRepository
    {
        Task<IEnumerable<Item>> GetInitialItemsAsync(int limit);

        Task<IEnumerable<Item>> GetNextItemsAsync(int limit, int nextCursor);

        Task AddItemsAsync(IEnumerable<AddItem> addItems);

        Task UpdateItemAysnc(Item item);

        Task DeleteItemAsync(int id);
    }
}
