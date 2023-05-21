using SurveySheet.Services.Models;

namespace SurveySheet.Services.Interfaces
{
    public interface ISheetService
    {
        Task<IEnumerable<ItemDto>> GetItemsAsync(int limit, int? nextCursor, int? userId = null);

        Task AddItemsAsync(IEnumerable<AddItemDto> addItemDto);

        Task UpdateItemAsync(UpdateItemDto updateItemDto);

        Task DeleteItemAsync(int id);

        Task CheckItemAsync(int userId, int id);
    }
}
