using SurveySheet.Services.Models;

namespace SurveySheet.Services
{
    public interface ISheetService
    {
        public Task<IEnumerable<ItemDto>> GetItemsAsync(int limit, int? nextCursor);
    }
}
