using SurveySheet.Services.Models;

namespace SurveySheet.Services.Interfaces
{
    public interface ISheetService
    {
        Task<IEnumerable<ItemDto>> GetItemsAsync(int limit, int? nextCursor);

        Task AddItemsAsync(IEnumerable<AddItemDto> addItemDto);
    }
}
